using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Hotel
{
    public decimal Hotelid { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public decimal? Rating { get; set; }

    public string? Hotelsdescription { get; set; }

    public decimal? Countryid { get; set; }

    public decimal? Cityid { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual City? City { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Hotelamenity> Hotelamenities { get; set; } = new List<Hotelamenity>();

    public virtual ICollection<Hotelservice> Hotelservices { get; set; } = new List<Hotelservice>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
