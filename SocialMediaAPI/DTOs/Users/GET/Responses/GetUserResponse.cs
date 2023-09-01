using SocialMediaAPI.HATEOAS;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.DTOs.Users.GET.Responses
{
    public class GetUserResponse:LinkCollection
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }

        public List<FollowedUser> Followed { get; set; }

        public List<FollowedUser> Followers { get; set; }

        public GetUserResponse()
        {

        }

    }
}
