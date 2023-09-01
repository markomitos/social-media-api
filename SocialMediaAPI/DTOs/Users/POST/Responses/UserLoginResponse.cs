using SocialMediaAPI.HATEOAS;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.DTOs.Users.POST.Responses
{
    public class UserLoginResponse:LinkCollection
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }

    }
}
