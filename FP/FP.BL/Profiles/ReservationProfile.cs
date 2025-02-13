using AutoMapper;
using FP.BL.Dtos.Reservation;
using FP.Core.Entities;

namespace FP.BL.Profiles;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<ReservationCreateDto, Reservation>();
        CreateMap<ReservationUpdateDto, Reservation>().ReverseMap();

    }
}
