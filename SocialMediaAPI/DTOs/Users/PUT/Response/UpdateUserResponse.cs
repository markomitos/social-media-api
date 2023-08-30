using SocialMediaAPI.Models;

namespace SocialMediaAPI.DTOs.Users.PUT.Response
{
    public class UpdateUserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }

        public UpdateUserResponse()
        {

        }

        public UpdateUserResponse(User user)
        {
            Id = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
    }
}
