using SocialMediaAPI.Models;

namespace SocialMediaAPI.DTOs.Users.GET.Responses
{
    public class GetUserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }

        public GetUserResponse()
        {

        }

        public GetUserResponse(User user)
        {
            Id = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
    }
}
