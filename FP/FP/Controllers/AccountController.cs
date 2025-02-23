using FP.BL.Dtos.User;
using FP.BL.Exceptions.Common;
using FP.BL.Services.Interfaces;
using FP.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace FP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(SignInManager<User> _signInManager, IAuthService _service) : ControllerBase
    {
        private bool isAuthenticated => HttpContext.User.Identity?.IsAuthenticated ?? false;

        [HttpPost("[action]")]
        public async Task<IActionResult> Register( RegisterDto dto)
        {
            //if (!ModelState.IsValid) {
            //    ModelState.AddModelError("", "");
            //}
            if (isAuthenticated)
                throw new InvalidException("you alread logged in");

            await _service.RegisterAsync(dto);
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (isAuthenticated)
                throw new InvalidException("you alread logged in");

            await _service.LoginAsync(dto);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
