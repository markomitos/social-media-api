using System.ComponentModel.DataAnnotations;

namespace SocialMediaAPI.DTOs.Users.PUT.Request
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }


        public string Password { get; set; }
    }
}
