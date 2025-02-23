using FP.BL.Dtos.Rating;
using FP.BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FP.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RatingsController(IRatingService _service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync());
    }
    [HttpPost]
    public async Task<IActionResult> Post(RatingCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> Put(int id, RatingUpdateDto dto)
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
