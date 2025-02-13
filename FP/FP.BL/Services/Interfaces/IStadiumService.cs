using FP.BL.Dtos.Stadium;
using FP.Core.Entities;

namespace FP.BL.Services.Interfaces;

public interface IStadiumService
{
    Task<IEnumerable<Stadium>> GetAllAsync();
    Task CreateAsync(StadiumCreateDto dto);
    Task UpdateAsync(StadiumUpdateDto dto);
    Task DeleteAsync(int id);
}
