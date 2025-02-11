using FP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetCurrentUser();
        int GetCurrentUserId();
        Task<User?> GetByUserNameAsync(string userName);

    }
}
