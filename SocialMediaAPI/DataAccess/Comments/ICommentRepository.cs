using SocialMediaAPI.Models;

namespace SocialMediaAPI.DataAccess.Comments
{
    public interface ICommentRepository
    {
        List<Comment> GetComments();

        List<Comment> GetCommentsByUser(int  userId);

        Comment? GetComment(int id);

        Comment? CreateComment(Comment comment);

        Comment? UpdateComment(Comment comment);

        bool DeleteComment(int id);

    }
}
