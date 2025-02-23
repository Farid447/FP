using System.ComponentModel.DataAnnotations;

namespace FP.BL.Dtos.Rating;

public class RatingCreateDto
{
    public int StadiumId { get; set; }
    
    [Required, MaxLength(5)]
    public int Rate { get; set; }

    [MaxLength(256)]
    public string? Feedback { get; set; }
}
