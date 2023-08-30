namespace SocialMediaAPI.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public List<string> Tags { get; set; }

        public int UserId { get; set; }

        public List<int> Likes { get; set; }

        public PostStatus Status { get; set; }

        public Post()
        {
            
        }

        public Post(Post post)
        {
            Id = post.Id;
            Content = post.Content;
            Tags = post.Tags;
            UserId = post.UserId;
            Likes = post.Likes;
            Status = post.Status;
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

        public void Like(int userId)
        {
            if (Likes.Contains(userId)) throw new Exception("User Already liked this post");
            Likes.Add(userId);
        }
    }
}
