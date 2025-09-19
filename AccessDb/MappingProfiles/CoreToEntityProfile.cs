using AccessDb.Entities;
using AutoMapper;
using Core.Models;

namespace AccessDb.MappingProfiles;

public class CoreToEntityProfile : Profile
{
    public CoreToEntityProfile()
    {
        CreateMap<CommentEntity, Comment>().ReverseMap();
        CreateMap<UserEntity, User>().ReverseMap();
        CreateMap<PostEntity, Post>().ReverseMap();
    }
}