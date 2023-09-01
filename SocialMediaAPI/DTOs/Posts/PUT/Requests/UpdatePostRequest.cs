using SocialMediaAPI.Models;

namespace SocialMediaAPI.DTOs.Posts.PUT.Requests
{
    public class UpdatePostRequest
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public List<TagDTO> Tags { get; set; }
    }
}
