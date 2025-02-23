using AutoMapper;
using FP.BL.Dtos.User;
using FP.Core.Entities;

namespace FP.BL.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterDto, User>();
        CreateMap<LoginDto, User>();
        CreateMap<UserUpdateDto, User>().ReverseMap();
    }
}
