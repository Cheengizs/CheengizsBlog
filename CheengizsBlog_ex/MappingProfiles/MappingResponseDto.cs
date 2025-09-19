using AutoMapper;
using CheengizsBlog_ex.Contracts;
using Core.Enums;
using Core.Models;

namespace CheengizsBlog_ex.MappingProfiles;

public class MappingResponseDto : Profile
{
    public MappingResponseDto()
    {
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

        CreateMap<UserResponse, User>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse<Roles>(src.Role)))
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

        CreateMap<PostResponse, Post>().ReverseMap();
        CreateMap<CommentResponse, Comment>().ReverseMap();
    }
}