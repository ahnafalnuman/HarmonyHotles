using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Amenity
{
    public decimal Amenityid { get; set; }

    public string? Amenityname { get; set; }

    public string? Amenitytype { get; set; }

    public virtual ICollection<Hotelamenity> Hotelamenities { get; set; } = new List<Hotelamenity>();

    public virtual ICollection<Roomamenity> Roomamenities { get; set; } = new List<Roomamenity>();
}
