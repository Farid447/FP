using FP.BL.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.BL.Services.Interfaces
{
    public interface IAuthService
    {
        public Task LoginAsync(LoginDto dto);
        public Task RegisterAsync(RegisterDto dto);
    }
}
