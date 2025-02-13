using FP.BL.Dtos.User;

namespace FP.BL.Services.Interfaces;

public interface IUserService
{
    Task<string> CreateAsync(RegisterDto dto);
    Task<bool> LoginAsync(LoginDto dto);
    Task DeleteAsync(string username);
}