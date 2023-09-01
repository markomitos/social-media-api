using Microsoft.Extensions.Hosting;
using SocialMediaAPI.DTOs.Comments.POST.Requests;
using SocialMediaAPI.DTOs.Posts.POST.Request;
using SocialMediaAPI.DTOs.Users.POST.Requests;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.Validators
{
    public static class Validator
    {
        public static bool IsValid(User user)
        {
            return user.Email is not null;
        }

        public static bool IsValid(UserLoginRequest user)
        {
            return user.Email is not null;
        }

        public static bool IsValid(CreateUserRequest user)
        {
            return user.Email is not null;
        }

        public static bool IsValid(Post post)
        {
            return post.Content is not null;
        }

        public static bool IsValid(CreatePostRequest post)
        {
            return post.Content is not null;
        }

        public static bool IsValid(Comment comment)
        {
            return comment.Content is not null;
        }

        public static bool IsValid(CreateCommentRequest comment)
        {
            return comment.Content is not null;
        }
    }
}
