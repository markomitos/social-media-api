using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.DTOs.Users;
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
            return _databaseContext.Users.Where(u=>u.Status == UserStatus.Active).Include(f=>f.Followed).ToList();
        }

        public User? GetUser(int id)
        {
            return _databaseContext.Users.Where(u => u.Id == id && u.Status == UserStatus.Active).Include(f => f.Followed).FirstOrDefault();
        }

        public User? GetUser(string email)
        {
            return _databaseContext.Users.Where(u => u.Email == email && u.Status == UserStatus.Active).Include(f => f.Followed).FirstOrDefault();
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

        public bool DeactivateAccount(int id)
        {
            var user = GetUser(id);
            if (user == null || !user.IsActive()) return false;
            user.DeactivateAccount();
            _databaseContext.SaveChanges();
            return true;
        }

        public bool ActivateAccount(int id)
        {
            var user = GetInactiveUser(id);
            if (user == null || user.IsActive()) return false;
            user.ActivateAccount();
            _databaseContext.SaveChanges();
            return true;
        }

        private User? GetInactiveUser(int id)
        {
            return _databaseContext.Users.Where(u => u.Id == id && u.Status == UserStatus.Inactive).Include(f => f.Followed).FirstOrDefault();
        }

        public User? GetUser(string firstName, string lastName)
        {
            return _databaseContext.Users.Where(u => u.FirstName == firstName && u.LastName == lastName && u.Status == UserStatus.Active).Include(f => f.Followed).FirstOrDefault();
        }

        public bool FollowUser(int followerId, int followedId)
        {
            var user = GetUser(followedId);
            var follower = GetUser(followerId);
            if (user == null) return false;
            follower.Follow(user);
            _databaseContext.SaveChanges();
            return true;
        }
    }
}
