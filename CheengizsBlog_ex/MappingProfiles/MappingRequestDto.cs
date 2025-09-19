using AutoMapper;
using CheengizsBlog_ex.Contracts;
using Core.Enums;
using Core.Models;

namespace CheengizsBlog_ex.MappingProfiles;

public class MappingRequestDto : Profile
{
    public MappingRequestDto()
    {
        CreateMap<User, UserRequest>().ReverseMap();

        CreateMap<PostRequest, Post>().ReverseMap();
        CreateMap<CommentRequest, Comment>().ReverseMap();
    }
}