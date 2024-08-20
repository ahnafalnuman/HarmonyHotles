using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Hotelservice
{
    public decimal Hotelserviceid { get; set; }

    public decimal? Hotelid { get; set; }

    public decimal? Servicesid { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public virtual Service? Services { get; set; }
}
