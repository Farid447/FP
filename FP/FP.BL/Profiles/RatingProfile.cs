using AutoMapper;
using FP.BL.Dtos.Rating;
using FP.BL.Dtos.Reservation;
using FP.Core.Entities;

namespace FP.BL.Profiles;

public class RatingProfile : Profile
{
    public RatingProfile()
    {
        CreateMap<RatingCreateDto, Rating>();
        CreateMap<RatingUpdateDto, Rating>().ReverseMap();
    }
}
