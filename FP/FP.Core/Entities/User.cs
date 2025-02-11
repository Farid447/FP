using Microsoft.AspNetCore.Identity;

namespace FP.Core.Entities;

public class User : IdentityUser
{
    public string Name { get; set; }
    public string Number { get; set; }
    public string Email { get; set; }
    public string FIN { get; set; }
    public string ImageUrl { get; set; } = "Default.png";
    public IEnumerable<Reservation>? Reservations { get; set; }
}
