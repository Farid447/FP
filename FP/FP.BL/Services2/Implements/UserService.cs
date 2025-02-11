using AutoMapper;
using BlogApp.BL.DTOs.UserDtos;
using BlogApp.BL.Exceptions.Common;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;

namespace BlogApp.BL.Services.Implements;

public class UserService(IMapper _mapper,IUserRepository _repo):IUserService
{
    public async Task<string> CreateAsync(RegisterDto dto)
    {
        var existUser = await _repo.GetFirstAsync(x => x.Email == dto.Email || x.Username == dto.Username);
        if (existUser != null) throw new ExistException<User>();
        
       User user = _mapper.Map<User>(dto);
       await _repo.AddAsync(user);
       await _repo.SaveAsync();
       return user.Username;
    }

    public Task DeleteAsync(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> LoginAsync(LoginDto dto)
    {
        var user = await _repo.GetFirstAsync(x => x.Username == dto.UsernameOrEmail);
        if (user == null) throw new NotFoundException<User>();
        if (user.Username != dto.UsernameOrEmail || user.PasswordHash != dto.Password) return false;
        return true;
    }
  
}