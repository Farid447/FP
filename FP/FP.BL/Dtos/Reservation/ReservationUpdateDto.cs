using System.ComponentModel.DataAnnotations;

namespace FP.BL.Dtos.Reservation;

public class ReservationUpdateDto
{
    [Required, MaxLength(5)]
    public int PitchNumber { get; set; }

    [Required, MaxLength(20)]
    public int RoomNumber { get; set; }
    public DateTime ReservationDate { get; set; }
}
