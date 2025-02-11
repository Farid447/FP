using System.ComponentModel.DataAnnotations;

namespace FP.BL.Dtos.User;

public class RegisterDto
{
    [Required, MaxLength(16)]
    public string Name { get; set; }

    [Required, MaxLength(64), EmailAddress]
    public string Email { get; set; }

    [Required, MaxLength(32)]
    public string Number { get; set; }

    [Required, MaxLength(32), DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required, MaxLength(32), DataType(DataType.Password), Compare(nameof(Password))]
    public string RePassword { get; set; }
}
