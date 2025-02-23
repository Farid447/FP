using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FP.BL.Constants;
using FP.BL.Dtos.Options;
using FP.BL.ExternalServices.Interfaces;
using FP.Core.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FP.BL.ExternalServices.Implements;

public class JwtTokenHandler : IJwtTokenHandler
{
    readonly JwtOptions opt;
    public JwtTokenHandler(IOptions<JwtOptions> _opt)
    {
        opt = _opt.Value;
    }
    public string CreateToken(User user, string role)
    {
        List<Claim> claims = [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimType.FullName, user.FullName),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimType.Role, role),
        ];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(opt.SecretKey));
        SigningCredentials cred = new(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken secToken = new(
            issuer: opt.Issuer,
            audience: opt.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            signingCredentials: cred
        );
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(secToken);
    }
}