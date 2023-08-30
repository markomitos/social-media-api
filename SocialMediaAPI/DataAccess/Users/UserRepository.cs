using SocialMediaAPI.Models;

namespace SocialMediaAPI.DataAccess.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<User> GetUsers()
        {
            return _databaseContext.Users.Where(u=>u.IsActive()).ToList();
        }

        public User? GetUser(int id)
        {
            var user = _databaseContext.Users.FirstOrDefault(u => u.Id == id && u.IsActive());
            return user;
        }

        public User? GetUser(string email)
        {
            return _databaseContext.Users.FirstOrDefault(u => u.Email == email && u.IsActive());
        }

        public User? CreateUser(User user)
        {
            _databaseContext.Users.Add(user);
            _databaseContext.SaveChanges();
            return GetUser(user.Id);
        }

        public User? Update(User user)
        {
            var oldUser = GetUser(user.Id);
            if (oldUser is null) return null;
            _databaseContext.Entry(oldUser).CurrentValues.SetValues(user);
            _databaseContext.SaveChanges();
            return GetUser(user.Id);
        }

        public bool Delete(int id)
        {
            var user = GetUser(id);
            if (user == null)return false;
            user.DeleteAccount();
            _databaseContext.SaveChanges();
            return true;
        }

        public User? LogIn(string email, string password)
        {
            var user = GetUser(email);
            if (user == null || user.Password != password) return null;
            return user;
        }

        public User? DeactivateAccount(int id)
        {
            var user = GetUser(id);
            if (user == null) return null;
            user.DeactivateAccount();
            _databaseContext.SaveChanges();
            return GetUser(id);
        }

        public User? ActivateAccount(int id)
        {
            var user = GetUser(id);
            if (user == null) return null;
            user.ActivateAccount();
            _databaseContext.SaveChanges();
            return GetUser(id);
        }

        public User? GetUser(string firstName, string lastName)
        {
            return _databaseContext.Users.FirstOrDefault(u => u.FirstName == firstName && u.LastName == lastName && u.IsActive());
        }

        public User? FollowUser(int followerId, int followedId)
        {
            var user = GetUser(followedId);
            var follower = GetUser(followerId);
            if (user == null) return null;
            follower.Follow(followedId);
            _databaseContext.SaveChanges();
            return GetUser(followedId);
        }
    }
}
