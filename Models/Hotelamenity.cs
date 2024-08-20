using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Hotelamenity
{
    public decimal Hotelamenityid { get; set; }

    public decimal? Amenityid { get; set; }

    public decimal? Hotelid { get; set; }

    public virtual Amenity? Amenity { get; set; }

    public virtual Hotel? Hotel { get; set; }
}
