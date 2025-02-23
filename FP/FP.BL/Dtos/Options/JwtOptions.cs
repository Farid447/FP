namespace FP.BL.Dtos.Options;

public class JwtOptions
{
    public const string Jwt = "JwtOptions";
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
}