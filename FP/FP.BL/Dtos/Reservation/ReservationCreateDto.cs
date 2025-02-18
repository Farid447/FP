namespace FP.BL.Dtos.Reservation;

public class ReservationCreateDto
{
    public int UserId { get; set; }
    public int StadiumId { get; set; }
    public int PitchNumber { get; set; }
    public int RoomNumber { get; set; }
    public DateTime ReservationDate { get; set; }
}
