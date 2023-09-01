using AutoMapper;
using SocialMediaAPI.DTOs.Users;
using SocialMediaAPI.DTOs.Users.GET.Responses;
using SocialMediaAPI.DTOs.Users.POST.Requests;
using SocialMediaAPI.DTOs.Users.POST.Responses;
using SocialMediaAPI.DTOs.Users.PUT.Request;
using SocialMediaAPI.DTOs.Users.PUT.Response;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.MappingProfiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, GetUserResponse>();
            CreateMap<User, UserLoginResponse>();
            CreateMap<User, UpdateUserResponse>();
            CreateMap<CreateUserRequest, User>();
            CreateMap<UserLoginResponse, User>();
            CreateMap<FollowedUser, User>();
            CreateMap<User,FollowedUser>();
            CreateMap<UpdateUserRequest, User>();
            CreateMap<UpdateUserRequest, UpdateUserResponse>();
        }

    }
}
