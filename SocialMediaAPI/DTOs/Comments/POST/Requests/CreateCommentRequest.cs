using SocialMediaAPI.Models;

namespace SocialMediaAPI.DTOs.Comments.POST.Requests
{
    public class CreateCommentRequest
    {
        public string Content { get; set; }

        public int PostId { get; set; }


        public int UserId { get; set; }

    }
}
