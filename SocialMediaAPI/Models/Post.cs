namespace SocialMediaAPI.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public List<Tag> Tags { get; set; }

        public int UserId { get; set; }

        public List<User> Likes { get; set; }

        public PostStatus Status { get; set; }

        public Post()
        {
            Status = PostStatus.Active;
            Likes ??= new List<User>();
            Tags ??= new List<Tag>();
        }

        public static void CopyUnchangedProperties(Post post1, Post post2)
        {
            post1.UserId = post2.UserId;
            post1.Likes = post2.Likes;
            post1.Status = post2.Status;
        }

        public bool IsActive()
        {
            return (Status == PostStatus.Active);
        }

        public void Delete()
        {
            Status = PostStatus.Deleted;
        }

        public void Archive()
        {
            Status = PostStatus.Archived;
        }

        public void Activate()
        {
            Status = PostStatus.Active;
        }

        public void Like(User user)
        {
            if (Likes.Contains(user)) throw new Exception("User Already liked this post");
            Likes.Add(user);
        }
    }
}
