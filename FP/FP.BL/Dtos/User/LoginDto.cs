using System.ComponentModel.DataAnnotations;

namespace FP.BL.Dtos.User;

public class LoginDto
{
    [Required, MaxLength(64)]
    public string EmailorFIN { get; set; }

    [Required, DataType(DataType.Password)]
    public string Password { get; set; }
    public bool RememberMe { get; set; } = false;

}
