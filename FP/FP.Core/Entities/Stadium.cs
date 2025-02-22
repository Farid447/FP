using FP.Core.Entities.Common;

namespace FP.Core.Entities;

public class Stadium : BaseEntity
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string LocationLink { get; set; }
    public string Address { get; set; }
    public string Description { get; set; } = "";
    public int RoomCount { get; set; } = 1;
    public int PitchCount { get; set; } = 1;
    public string ImageUrl { get; set; } = "Default.png";
    public List<string> ImageUrls { get; set; } = [];
    public IEnumerable<Reservation>? Reservations {  get; set; }
    public IEnumerable<Rating>? Ratings { get; set; }
}
