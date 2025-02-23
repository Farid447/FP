using AutoMapper;
using FP.BL.Dtos.Reservation;
using FP.BL.Exceptions.Common;
using FP.BL.Services.Interfaces;
using FP.Core.Entities;
using FP.Core.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FP.BL.Services.Implements;

public class ReservationService(IReservationRepository _repo, IMapper _mapper, IHttpContextAccessor _httpContext) : IReservationService
{
    private readonly HttpContext _httpcontext = _httpContext.HttpContext!;

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task CreateAsync(ReservationCreateDto dto)
    {
        Reservation Reservation = _mapper.Map<Reservation>(dto);

        var userId = _httpcontext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        Reservation.UserId = userId;

        await _repo.AddAsync(Reservation);
    }

    public async Task UpdateAsync(int id, ReservationUpdateDto dto)
    {
        var Reservation = await _repo.GetByIdAsync(id) ?? throw new NotFoundException<Reservation>();

        _mapper.Map(dto, Reservation);
        Reservation.UpdatedTime = DateTime.Now;
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Reservation? Reservation = await _repo.GetByIdAsync(id);
        if (Reservation != null)
        {
            await _repo.DeleteAsync(Reservation);
        }
    }
    public async Task ToggleAsync(int id)
    {
        await _repo.ToggleAsync(id);
    }
}
