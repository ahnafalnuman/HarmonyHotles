using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Destination
{
    public decimal Destinationid { get; set; }

    public string? Country { get; set; }

    public string? Imagepath { get; set; }

    public decimal? Placescount { get; set; }

    public string? Ullink { get; set; }
}
