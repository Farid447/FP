using FP.Core.Entities;

namespace FP.BL.ExternalServices.Interfaces;

public interface IJwtTokenHandler
{
    string CreateToken(User user, string role);
}