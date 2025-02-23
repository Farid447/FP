using FP.BL.Dtos.Stadium;
using FP.BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FP.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StadiumsController(IStadiumService _service) : ControllerBase
{
    [HttpGet]
    [Authorize("Host")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync());
    }
    [HttpPost]
    public async Task<IActionResult> Post(StadiumCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> Put(int id, StadiumUpdateDto dto)
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
