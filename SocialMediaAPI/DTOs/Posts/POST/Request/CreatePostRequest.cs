using SocialMediaAPI.Models;

namespace SocialMediaAPI.DTOs.Posts.POST.Request
{
    public class CreatePostRequest
    {
        public string Content { get; set; }

        public List<TagDTO> Tags { get; set; }

        public int UserId { get; set; }
        
    }
}
