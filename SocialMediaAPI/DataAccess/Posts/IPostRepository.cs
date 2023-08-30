using Microsoft.Identity.Client;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.DataAccess.Posts
{
    public interface IPostRepository
    {
        List<Post> GetPosts();

        Post? GetPost(int id);

        List<Post> GetPostsByCreator(int  creatorId);

        List<Post> GetPostsByTag(string tag);
        
        Post? CreatePost(Post post);

        Post? UpdatePost(Post post);

        bool DeletePost(int id);

        Post? ArchivePost(int id);

        Post? ActivatePost(int id);

        List<Post> GetMostPopularPosts(int maxPostsNumber);

        Post? LikePost(int postId,int userId);





    }
}
