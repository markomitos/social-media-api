using SocialMediaAPI.DTOs.Users;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.DTOs.Posts.PUT.Response
{
    public class UpdatePostResponse
    {
        public string Content { get; set; }

        public List<Tag> Tags { get; set; }

        public int UserId { get; set; }

        public List<FollowedUser> Likes { get; set; }

        public PostStatus Status { get; set; }
    }
}
