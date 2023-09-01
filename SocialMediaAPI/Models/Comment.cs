using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialMediaAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        
        public int PostId { get; set; }
        
        
        public int UserId { get; set; }
        
        

        public CommentStatus Status { get; set; }

        public Comment()
        {

        }

        public Comment(Comment comment)
        {
            Id = comment.Id;
            Content = comment.Content;
            PostId = comment.PostId;
            UserId = comment.UserId;
            Status = comment.Status;
        }

        public bool IsActive()
        {
            return Status == CommentStatus.Active;
        }

        public void Delete()
        {
            Status = CommentStatus.Deleted;
        }

        public static void CopyUnchangedProperties(Comment comment1, Comment comment2)
        {
            comment1.UserId = comment2.UserId;
            comment1.PostId = comment2.PostId;
            comment1.Status = comment2.Status;
        }
    }
}
