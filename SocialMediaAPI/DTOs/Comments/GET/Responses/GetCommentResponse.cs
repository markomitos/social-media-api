using SocialMediaAPI.Models;

namespace SocialMediaAPI.DTOs.Comments.GET.Responses
{
    public class GetCommentResponse
    {
        public string Content { get; set; }

        public int PostId { get; set; }


        public int UserId { get; set; }


    }
}
