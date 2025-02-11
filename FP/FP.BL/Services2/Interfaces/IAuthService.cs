using BlogApp.BL.DTOs.UserDtos;

namespace BlogApp.BL.Services.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterDto dto);
    Task<string> LoginAsync(LoginDto dto);
}