using SocialMediaAPI.DTOs.Users;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.DTOs.Posts.GET.Responses
{
    public class GetPostResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public List<Tag> Tags { get; set; }

        public int UserId { get; set; }

        public List<FollowedUser> Likes { get; set; }

        public PostStatus Status { get; set; }
    }
}
