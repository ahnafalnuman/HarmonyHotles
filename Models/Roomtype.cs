using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarmonyHotles.Models;

public partial class Roomtype
{
    public decimal Roomtypeid { get; set; }

    public string Typename { get; set; } = null!;

    public string? Roomtypesdescription { get; set; }

    public decimal? Roomsize { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    [NotMapped]
    public virtual ICollection<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
