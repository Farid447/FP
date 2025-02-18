using FP.BL.Dtos.Reservation;
using FP.BL.Dtos.Stadium;
using FP.Core.Entities;

namespace FP.BL.Services.Interfaces;

public interface IReservationService
{
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task CreateAsync(ReservationCreateDto dto);
    Task UpdateAsync(int id, ReservationUpdateDto dto);
    Task DeleteAsync(int id);
}
