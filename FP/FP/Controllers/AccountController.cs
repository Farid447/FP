using FP.BL.Dtos.User;
using FP.BL.Exceptions.Common;
using FP.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace FP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(SignInManager<User> _signInManager) : ControllerBase
    {
        private bool isAuthenticated => HttpContext.User.Identity?.IsAuthenticated ?? false;
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            //if (!ModelState.IsValid) {
            //    ModelState.AddModelError("", "");
            //}
            if (isAuthenticated)
                throw new InvalidException("you alread logged in");

            return Ok();
        }

        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (isAuthenticated)
                throw new InvalidException("you alread logged in");

            return Ok();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
