﻿using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class City
{
    public decimal Cityid { get; set; }

    public string Cityname { get; set; } = null!;

    public decimal? Countryid { get; set; }

    public string? Imagepath { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}