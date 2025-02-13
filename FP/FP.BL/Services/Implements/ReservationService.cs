using FP.BL.Dtos.Reservation;
using FP.BL.Dtos.Stadium;
using FP.BL.Services.Interfaces;
using FP.Core.Entities;
using FP.Core.Repositories;

namespace FP.BL.Services.Implements;

public class ReservationService(IReservationRepository _repo) : IReservationService
{
    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }
    public Task CreateAsync(ReservationCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        Reservation? reservation = await _repo.GetByIdAsync(id);
        if (reservation != null)
            await _repo.DeleteAsync(reservation);
    }


    public Task UpdateAsync(ReservationUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
