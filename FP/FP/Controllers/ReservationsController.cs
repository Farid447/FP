using FP.BL.Dtos.Reservation;
using FP.BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FP.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReservationsController(IReservationService _service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync());
    }
    [HttpPost]
    public async Task<IActionResult> Post(ReservationCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ReservationUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return Ok();
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}
