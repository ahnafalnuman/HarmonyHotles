using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Image
{
    public decimal Imageid { get; set; }

    public string? Imagepath { get; set; }

    public decimal? Hotelid { get; set; }

    public decimal? Roomid { get; set; }

    public decimal? Roomtypeid { get; set; }

    public decimal? Eventid { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public virtual Room? Room { get; set; }

    public virtual Roomtype? Roomtype { get; set; }
}
