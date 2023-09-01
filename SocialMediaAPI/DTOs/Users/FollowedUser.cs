using System.ComponentModel.DataAnnotations;

namespace SocialMediaAPI.DTOs.Users
{
    public class FollowedUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
