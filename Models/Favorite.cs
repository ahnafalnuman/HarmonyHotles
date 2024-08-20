using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Favorite
{
    public decimal Favoriteid { get; set; }

    public decimal? Customerid { get; set; }

    public decimal? Hotelid { get; set; }

    public decimal? Eventid { get; set; }

    public DateTime? Addeddate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Hotel? Hotel { get; set; }
}
