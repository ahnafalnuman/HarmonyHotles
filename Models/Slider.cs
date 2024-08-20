using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarmonyHotles.Models;

public partial class Slider
{
    public decimal Sliderid { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Imagepath { get; set; }

    public string? Buttontext { get; set; }

    public string? Buttonlink { get; set; }

    public bool? Isactive { get; set; }

    public decimal? Displayorder { get; set; }

    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }
}
