using System.ComponentModel.DataAnnotations;

namespace FP.BL.Dtos.Rating;

public class RatingUpdateDto
{
    [Required, MaxLength(5)]
    public int Rate { get; set; }

    [MaxLength(256)]
    public string? Feedback { get; set; }
}
