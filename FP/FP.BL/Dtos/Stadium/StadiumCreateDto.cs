﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FP.BL.Dtos.Stadium;

public class StadiumCreateDto
{
    [Required, MaxLength(32)]
    public string Name { get; set; }

    [Required, MaxLength(32)]
    public string PhoneNumber { get; set; }

    [Required, MaxLength(128)]
    public string Address { get; set; }

    [Required, MaxLength(128)]
    public string LocationLink { get; set; }

    [MaxLength(128)]
    public string? Description { get; set; } = "";

    [MaxLength(20)]
    public int RoomCount { get; set; }

    [MaxLength(5)]
    public int StadiumCount { get; set; }
    public IFormFile? Image { get; set; }
    public IEnumerable<IFormFile>? Images { get; set; }
}
