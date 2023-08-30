using System.ComponentModel.DataAnnotations;

namespace SocialMediaAPI.DTOs.Users.POST.Requests
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }


        public string Password { get; set; }
    }
}
