using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarmonyHotles.Models;

public partial class Country
{
    public decimal Countryid { get; set; }

    public string Countryname { get; set; } = null!;

    public string? Imagepath { get; set; }


    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}
