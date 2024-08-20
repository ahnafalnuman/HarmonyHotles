using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Roomamenity
{
    public decimal Roomamenityid { get; set; }

    public decimal? Roomid { get; set; }

    public decimal? Amenityid { get; set; }

    public virtual Amenity? Amenity { get; set; }

    public virtual Room? Room { get; set; }
}
