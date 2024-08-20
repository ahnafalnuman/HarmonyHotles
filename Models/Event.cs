using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Event
{
    public decimal Eventid { get; set; }

    public decimal? Countryid { get; set; }

    public decimal? Hotelid { get; set; }

    public decimal? Cityid { get; set; }

    public string Name { get; set; } = null!;

    public string? Eventsdescription { get; set; }

    public string? Location { get; set; }

    public string Ticketprice { get; set; } = null!;

    public DateTime? Timefrom { get; set; }

    public DateTime? Timeto { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual City? City { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual Hotel? Hotel { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
