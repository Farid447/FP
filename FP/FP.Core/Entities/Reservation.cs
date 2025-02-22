using FP.Core.Entities.Common;

namespace FP.Core.Entities;

public class Reservation : BaseEntity
{
    public int? UserId { get; set; }
    public User? User { get; set; }
    public int? StadiumId { get; set; }
    public Stadium? Stadium { get; set; }
    public int PitchNumber { get; set; }
    public int RoomNumber { get; set; }
    public DateTime ReservationDate { get; set; }
}
