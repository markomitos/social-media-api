using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
            return _databaseContext.Posts.Where(p=>p.Status == PostStatus.Active).Include(p=>p.Likes).Include(p=>p.Tags).ToList();
        }

        public Post? GetPost(int id)
        {
            return _databaseContext.Posts.Where(p => p.Id == id&& p.Status == PostStatus.Active).Include(p=>p.Likes).Include(p => p.Tags).FirstOrDefault();
        }

        public List<Post> GetPostsByUser(int creatorId)
        {
            return _databaseContext.Posts.Where(p => p.UserId == creatorId && p.Status == PostStatus.Active).Include(p => p.Likes).Include(p => p.Tags).ToList();
        }

        public List<Post> GetPostsByTag(string tag)
        {
            return _databaseContext.Posts.Where(p => p.Tags.Any(t=>t.Content.Contains(tag)) && p.Status == PostStatus.Active).Include(p => p.Likes).Include(p => p.Tags).ToList();
        }

        public Post? CreatePost(Post post)
        {
            AddMissingTags(post);
            _databaseContext.Posts.Add(post);
            _databaseContext.SaveChanges();
            return GetPost(post.Id);
        }

        private void AddMissingTags(Post post)
        {
            foreach (var tag in post.Tags.Where(tag => !_databaseContext.Tags.Contains(tag)))
            {
                _databaseContext.Tags.Add(tag);
            }
        }

        private void BindTags(Post post)
        {
            List<Tag> tags = new List<Tag>();
            foreach (var tag in post.Tags)
            {
                tags.Add(GetTag(tag.Content));
            }

            post.Tags.RemoveAll(t=>t.Id==0);
            post.Tags.AddRange(tags);
        }

        private Tag? GetTag(string tagContent)
        {
            return _databaseContext.Tags.FirstOrDefault(t => t.Content.Equals(tagContent));
        }

        public Post? UpdatePost(Post post)
        {
            var oldPost = GetPost(post.Id);
            if (oldPost == null) return null;
            AddMissingTags(post);
            BindTags(post);
            Post.CopyUnchangedProperties(post,oldPost);
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

        public bool ArchivePost(int id)
        {
            var post = GetPost(id);
            if (post == null) return false;
            post.Archive();
            _databaseContext.SaveChanges();
            return true;
        }

        public bool ActivatePost(int id)
        {
            var post = GetArchivedPost(id);
            if (post == null) return false;
            post.Activate();
            _databaseContext.SaveChanges();
            return true;
        }

        private Post? GetArchivedPost(int id)
        {
            return _databaseContext.Posts.Where(p => p.Id == id && p.Status == PostStatus.Archived).Include(p => p.Likes).Include(p => p.Tags).FirstOrDefault();
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
            var posts = _databaseContext.Posts.OrderByDescending(p => p.Likes.Count).Include(p=>p.Likes).Include(p => p.Tags).ToList();
            //posts.Sort(ComparePostsByLikes);
            return posts.Take(maxPostsNumber).ToList();
        }

        public bool LikePost(int postId,User user)
        {
            var post = GetPost(postId);
            if (post == null) return false;
            post.Like(user);
            _databaseContext.SaveChanges();
            return true;
        }
    }
}
