using AutoMapper;
using SocialMediaAPI.DTOs.Comments.GET.Responses;
using SocialMediaAPI.DTOs.Comments.POST.Requests;
using SocialMediaAPI.DTOs.Comments.PUT.Requests;
using SocialMediaAPI.DTOs.Comments.PUT.Responses;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.MappingProfiles
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, GetCommentResponse>();
            CreateMap<Comment, UpdateCommentResponse>();
            CreateMap<CreateCommentRequest, Comment>();
            CreateMap<UpdateCommentRequest, Comment>();
        }
    }
}
