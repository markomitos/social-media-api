using AutoMapper;
using SocialMediaAPI.DTOs.Posts.GET.Responses;
using SocialMediaAPI.DTOs.Posts.POST.Request;
using SocialMediaAPI.DTOs.Posts.PUT.Requests;
using SocialMediaAPI.DTOs.Posts.PUT.Response;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.MappingProfiles
{
    public class PostProfile:Profile
    {
        public PostProfile()
        {
            CreateMap<Post, GetPostResponse>();
            CreateMap<Post, UpdatePostResponse>();
            CreateMap<CreatePostRequest, Post>();
            CreateMap<UpdatePostRequest, Post>();
            CreateMap<UpdatePostRequest, UpdatePostResponse>();
        }
    }
}
