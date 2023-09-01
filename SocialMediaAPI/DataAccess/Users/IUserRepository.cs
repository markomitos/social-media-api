using SocialMediaAPI.Models;

namespace SocialMediaAPI.DataAccess.Users
{
    public interface IUserRepository
    {
        public List<User> GetUsers();


        public User? GetUser(int id);

        public User? GetUser(string email);

        public User? CreateUser(User user);

        public User? Update(User user);

        public bool Delete(int id);

        public User? LogIn(string email, string password);


        public bool DeactivateAccount(int id);

        public bool ActivateAccount(int id);

        public User? GetUser(string firstName, string lastName);

        public bool FollowUser(int followerId, int followedId);
    }
}
