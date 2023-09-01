using SocialMediaAPI.Models;

namespace SocialMediaAPI.DTOs.Comments.PUT.Responses
{
    public class UpdateCommentResponse
    {
        public string Content { get; set; }

        public int PostId { get; set; }


        public int UserId { get; set; }


    }
}
