using Microsoft.Identity.Client;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.DataAccess.Posts
{
    public interface IPostRepository
    {
        List<Post> GetPosts();

        Post? GetPost(int id);

        List<Post> GetPostsByUser(int  creatorId);

        List<Post> GetPostsByTag(string tag);
        
        Post? CreatePost(Post post);

        Post? UpdatePost(Post post);

        bool DeletePost(int id);

        bool ArchivePost(int id);

        bool ActivatePost(int id);

        List<Post> GetMostPopularPosts(int maxPostsNumber);

        bool LikePost(int postId,User user);





    }
}
