using FP.Core.Entities.Common;

namespace FP.Core.Entities;

public class Rating : BaseEntity
{
    public double Rate { get; set; }
    public string? Feedback { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public int? StadiumId { get; set; }
    public Stadium? Stadium { get; set; }
}
