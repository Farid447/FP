using AutoMapper;
using FP.BL.Dtos.Reservation;
using FP.BL.Dtos.User;
using FP.BL.Extentions;
using FP.Core.Entities;

namespace FP.BL.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterDto, User>();
        CreateMap<LoginDto, User>();
    }
}
