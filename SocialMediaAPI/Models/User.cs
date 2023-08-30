using System.ComponentModel.DataAnnotations;

namespace SocialMediaAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }


        public string Password { get; set; }

        public UserStatus Status { get; set; }

        public List<int> FollowedUsersIds { get; set; }

        public User()
        {

        }

        public User(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Password = user.Password;
            Status = user.Status;
            FollowedUsersIds = new List<int>();
            FollowedUsersIds.AddRange(user.FollowedUsersIds.ToList());
        }

        public void DeactivateAccount()
        {
            Status = UserStatus.Inactive;
        }

        public void ActivateAccount()
        {
            Status = UserStatus.Active;
        } 

        public void DeleteAccount()
        {
            Status = UserStatus.Deleted;
        }

        public bool IsActive()
        {
            return Status == UserStatus.Active;
        }

        public void Follow(int followedId)
        {
            FollowedUsersIds.Add(followedId);
        }
    }
}
