using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FP.BL.Dtos.User;

public class RegisterDto
{
    [Required, MaxLength(32)]
    public string FullName { get; set; }

    [Required, MinLength(7), MaxLength(7)]
    public string FIN { get; set; }

    [Required, MaxLength(64), EmailAddress]
    public string Email { get; set; }

    [Required, MaxLength(32)]
    public string PhoneNumber { get; set; }
    public IFormFile? Image { get; set; }

    [Required, MaxLength(32), DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required, MaxLength(32), DataType(DataType.Password), Compare(nameof(Password))]
    public string RePassword { get; set; }
}
