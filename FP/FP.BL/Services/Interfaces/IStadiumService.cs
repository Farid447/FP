using FP.BL.Dtos.Stadium;
using FP.Core.Entities;

namespace FP.BL.Services.Interfaces;

public interface IStadiumService
{
    Task<IEnumerable<Stadium>> GetAllAsync();
    Task CreateAsync(StadiumCreateDto dto);
    Task UpdateAsync(int id, StadiumUpdateDto dto);
    Task DeleteAsync(int id);
    Task DeleteImgs(int id, IEnumerable<string> imgs);
    Task ToggleAsync(int id);
}
