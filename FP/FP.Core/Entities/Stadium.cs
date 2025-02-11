using FP.Core.Entities.Common;

namespace FP.Core.Entities;

public class Stadium : BaseEntity
{
    public string LocationLink { get; set; }
    public string Address { get; set; }
    public int RoomCount { get; set; } = 1;
    public int StadiumCount { get; set; } = 1;
    public string ImageUrl { get; set; } = "Default.png";
    public IEnumerable<string> ImageUrls { get; set; } = [];
    public IEnumerable<Reservation>? Reservations {  get; set; } 
}
