using AutoMapper;
using FP.BL.Dtos.Stadium;
using FP.Core.Entities;

namespace FP.BL.Profiles;

public class StadiumProfile : Profile
{
    public StadiumProfile()
    {
        CreateMap<Stadium, StadiumCreateDto>()
            .ForMember(x => x.Description, y => y.MapFrom(t => t.Description ?? ""));
        CreateMap<Stadium, StadiumUpdateDto>().ReverseMap();
    }
}
