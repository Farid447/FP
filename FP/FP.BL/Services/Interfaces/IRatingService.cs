using FP.BL.Dtos.Rating;
using FP.Core.Entities;

namespace FP.BL.Services.Interfaces;

public interface IRatingService
{
    Task<IEnumerable<Rating>> GetAllAsync();
    Task CreateAsync(RatingCreateDto dto);
    Task UpdateAsync(int id, RatingUpdateDto dto);
    Task DeleteAsync(int id);
}
