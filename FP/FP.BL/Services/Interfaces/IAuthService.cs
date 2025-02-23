using FP.BL.Dtos.User;

namespace FP.BL.Services.Interfaces;

public interface IAuthService
{
    public Task RegisterAsync(RegisterDto dto);
    public Task<string> LoginAsync(LoginDto dto);
    public Task UpdateUserAsync(int id, UserUpdateDto dto);
}
