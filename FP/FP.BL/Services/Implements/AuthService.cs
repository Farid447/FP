using AutoMapper;
using FP.BL.Dtos.User;
using FP.BL.Exceptions.Common;
using FP.BL.Exceptions.FileExceptions;
using FP.BL.Extensions;
using FP.BL.ExternalServices.Interfaces;
using FP.BL.Services.Interfaces;
using FP.Core.Entities;
using FP.Core.Enums;
using FP.DAL.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FP.BL.Services.Implements;

public class AuthService(UserManager<User> _userManager,
    SignInManager<User> _signInManager, IMapper _mapper,
    IWebHostEnvironment _env, IJwtTokenHandler _jwtTokenHandler,
    IHttpContextAccessor _httpContext) : IAuthService
{
    private readonly HttpContext _httpcontext = _httpContext.HttpContext!;
    public async Task RegisterAsync(RegisterDto dto)
    {
        if (dto.Image != null)
            if (!dto.Image.IsValid())
                throw new TypeorSizeValidException();

        User user = _mapper.Map<User>(dto);
        
        if(dto.Image != null)
            user.ImageUrl = dto.Image.UploadAsync(_env.WebRootPath, "Imgs").Result;

        user.PassportImageUrl = dto.PassportImage.UploadAsync(_env.WebRootPath, "Imgs", "PassportImgs").Result;

        var result = await _userManager.CreateAsync(user, dto.Password);
        
        if (!result.Succeeded)
        {    
            string errors = "";
            
            foreach (var item in result.Errors)
                errors += (item + ". ");

            throw new InvalidException(errors);
        }

        var RoleResult = await _userManager.AddToRoleAsync(user, nameof(Roles.User));

        if (!RoleResult.Succeeded)
        {
            string errors = "";

            foreach (var item in RoleResult.Errors)
                errors += (item + ". ");

            throw new InvalidException(errors);
        }
    }
    public async Task<string> LoginAsync(LoginDto dto)
    {
        User? user;
        if (dto.EmailorFIN.Contains('@'))
            user = await _userManager.FindByEmailAsync(dto.EmailorFIN)
                ?? throw new InvalidException("Email is wrong");

        else if (dto.EmailorFIN.Length == 7)
            user = await _userManager.Users.FirstOrDefaultAsync(x => x.FIN == dto.EmailorFIN)
                ?? throw new InvalidException("FIN is wrong");

        else
            throw new InvalidException("Write email or FIN!");


        var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, true);

        if (!result.Succeeded)
            if (result.IsLockedOut)
                throw new InvalidException("Wait until" + user.LockoutEnd!.Value.ToString("yyyy-MM-dd HH:mm:ss"));

        var role = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenHandler.CreateToken(user, role[0]);
        return token;
    }

    public async Task UpdateUserAsync(int id, UserUpdateDto dto)
    {
        if (dto.Image != null)
            if (!dto.Image.IsValid())
                throw new TypeorSizeValidException();

        if (dto.PassportImage != null)
        {
            if (dto.PassportImage.IsValid())
                throw new TypeorSizeValidException();
        }

        var userId = _httpcontext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.Id == userId) ?? throw new InvalidException("User not found!");

        if (dto.Image != null)
            user.ImageUrl = dto.Image.UploadAsync(_env.WebRootPath, "Imgs").Result;

        if (dto.PassportImage != null)
            user.PassportImageUrl = dto.PassportImage.UploadAsync(_env.WebRootPath, "Imgs", "PassportImgs").Result;

        _mapper.Map(dto, user);
        //await _context.SaveChangesAsync();
    }
}
