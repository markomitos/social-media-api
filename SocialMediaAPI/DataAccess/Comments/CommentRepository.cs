using SocialMediaAPI.Models;

namespace SocialMediaAPI.DataAccess.Comments
{
    public class CommentRepository:ICommentRepository
    {
        private readonly DatabaseContext _databaseContext;

        public CommentRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public List<Comment> GetComments()
        {
            return _databaseContext.Comments.Where(c=>c.Status == CommentStatus.Active).ToList();
        }

        public List<Comment> GetCommentsByUser(int userId)
        {
            //return new List<Comment>();
            return _databaseContext.Comments.Where(c => c.Status == CommentStatus.Active&&c.UserId == userId).ToList();
        }

        public Comment? GetComment(int id)
        {
            return _databaseContext.Comments.FirstOrDefault(c => c.Id == id && c.Status == CommentStatus.Active);
        }

        public Comment? CreateComment(Comment comment)
        {
            _databaseContext.Comments.Add(comment);
            _databaseContext.SaveChanges();
            return GetComment(comment.Id);
        }

        public Comment? UpdateComment(Comment comment)
        {
            var oldComment = GetComment(comment.Id);
            if (oldComment == null) return null;
            Comment.CopyUnchangedProperties(comment,oldComment);
            _databaseContext.Entry(oldComment).CurrentValues.SetValues(comment);
            _databaseContext.SaveChanges();
            return GetComment(comment.Id);
        }

        public bool DeleteComment(int id)
        {
            var comment = GetComment(id);
            if (comment == null) return false;
            comment.Delete();
            _databaseContext.SaveChanges();
            return true;
        }
    }
}
