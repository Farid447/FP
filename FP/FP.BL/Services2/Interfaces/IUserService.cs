using BlogApp.BL.DTOs.UserDtos;
using BlogApp.Core.Entities;

namespace BlogApp.BL.Services.Interfaces;

public interface IUserService
{
    Task<string> CreateAsync(RegisterDto dto);
    Task<bool> LoginAsync(LoginDto dto);
    Task DeleteAsync(string username);
}