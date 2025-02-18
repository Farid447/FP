using Microsoft.AspNetCore.Identity;

namespace FP.Core.Entities;

public class User : IdentityUser
{
    public string FullName { get; set; }
    public string FIN { get; set; }
    public string ImageUrl { get; set; } = "Default.png";
    public IEnumerable<Reservation>? Reservations { get; set; }
}
