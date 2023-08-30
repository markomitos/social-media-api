using SocialMediaAPI.Models;

namespace SocialMediaAPI.DataAccess.Posts
{
    public class PostRepository:IPostRepository
    {
        private readonly DatabaseContext _databaseContext;

        public PostRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<Post> GetPosts()
        {
            return _databaseContext.Posts.Where(p=>p.IsActive()).ToList();
        }

        public Post? GetPost(int id)
        {
            return _databaseContext.Posts.FirstOrDefault(p => p.Id == id&& p.IsActive());
        }

        public List<Post> GetPostsByCreator(int creatorId)
        {
            return _databaseContext.Posts.Where(p => p.UserId == creatorId && p.IsActive()).ToList();
        }

        public List<Post> GetPostsByTag(string tag)
        {
            return _databaseContext.Posts.Where(p => p.Tags.Contains(tag)&& p.IsActive()).ToList();
        }

        public Post? CreatePost(Post post)
        {
            _databaseContext.Posts.Add(post);
            _databaseContext.SaveChanges();
            return GetPost(post.Id);
        }

        public Post? UpdatePost(Post post)
        {
            var oldPost = GetPost(post.Id);
            if (oldPost != null) return null;
            _databaseContext.Entry(oldPost).CurrentValues.SetValues(post);
            _databaseContext.SaveChanges();
            return GetPost(post.Id);

        }

         public bool DeletePost(int id)
        {
            var post = GetPost(id);
            if (post == null) return false;
            post.Delete();
            _databaseContext.SaveChanges();
            return true;
        }

        public Post? ArchivePost(int id)
        {
            var post = GetPost(id);
            if (post == null) return null;
            post.Archive();
            _databaseContext.SaveChanges();
            return GetPost(id);
        }

        public Post? ActivatePost(int id)
        {
            var post = GetPost(id);
            if (post == null) return null;
            post.Activate();
            _databaseContext.SaveChanges();
            return GetPost(id);
        }

        public static int ComparePostsByLikes(Post post1, Post post2)
        {
            if (post1 == null)
            {
                if (post2 == null) return 0;
                else return -1;
            }
            else
            {
                if (post2 == null) return 1;
                else return post1.Likes.Count.CompareTo(post2.Likes.Count);
            }

        }

        public List<Post> GetMostPopularPosts(int maxPostsNumber)
        {
            var posts = _databaseContext.Posts.ToList();
            posts.Sort(ComparePostsByLikes);
            return posts.Take(maxPostsNumber).ToList();
        }

        public Post? LikePost(int postId,int userId)
        {
            var post = GetPost(postId);
            if (post == null) return null;
            post.Like(userId);
            _databaseContext.SaveChanges();
            return GetPost(postId);
        }
    }
}
