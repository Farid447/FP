namespace FP.BL.Dtos.Reservation;

public class ReservationCreateDto
{
    public int SdadiumId { get; set; }
    public int StadiumNumber { get; set; }
    public int RoomNumber { get; set; }
    public DateTime ReservationDate { get; set; }
}
