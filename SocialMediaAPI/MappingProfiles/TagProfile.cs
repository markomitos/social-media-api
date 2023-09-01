using AutoMapper;
using SocialMediaAPI.DTOs.Posts;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.MappingProfiles
{
    public class TagProfile:Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagDTO>();
            CreateMap<TagDTO, Tag>();
        }
    }
}
