using FP.Core.Entities;
using FP.Core.Repositories;
using FP.DAL.Context;
//using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FP.DAL.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    //readonly HttpContext _httpContext;
    readonly FPDbContext _context;

    public UserRepository(FPDbContext context) : base(context)
    {
        _context = context;
        //_httpContext = httpContext.HttpContext;
    }
    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _context.Users.Where(x => x.Email == userName).FirstOrDefaultAsync();
    }
    public User GetCurrentUser()
    {
        throw new NotImplementedException();
    }

    public int GetCurrentUserId()
    {
        throw new NotImplementedException();
    }
}
