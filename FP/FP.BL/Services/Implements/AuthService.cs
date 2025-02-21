using AutoMapper;
using FP.BL.Dtos.User;
using FP.BL.Exceptions.Common;
using FP.BL.Extentions;
using FP.BL.Services.Interfaces;
using FP.Core.Entities;
using FP.Core.Enums;
using FP.DAL.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FP.BL.Services.Implements;

public class AuthService(UserManager<User> _userManager, SignInManager<User> _signInManager, IMapper _mapper, IWebHostEnvironment _env, FPDbContext _context) : IAuthService
{
    public async Task RegisterAsync(RegisterDto dto)
    {
        User user = _mapper.Map<User>(dto);
        
        if(dto.Image != null)
            user.ImageUrl = dto.Image.UploadAsync(_env.WebRootPath, "imgs").Result;

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
    public async Task LoginAsync(LoginDto dto)
    {
        User? user;
        if (dto.EmailorFIN.Contains('@'))
            user = await _userManager.FindByEmailAsync(dto.EmailorFIN)
                ?? throw new InvalidException("Email is wrong");
        
        else if(dto.EmailorFIN.Length == 7)
            user = await _context.Users.FirstOrDefaultAsync(x => x.FIN == dto.EmailorFIN)
                ?? throw new InvalidException("FIN is wrong");
        
        else
            throw new InvalidException("Write email or FIN!");


        var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, true);

        if (!result.Succeeded)
            if (result.IsLockedOut)
                throw new InvalidException("Wait until" + user.LockoutEnd!.Value.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}
