using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarmonyHotles.Models;

public partial class Room
{
    internal DateTime AvailableFrom;

    public decimal Roomid { get; set; }

    public decimal? Hotelid { get; set; }

    public decimal? Roomtypeid { get; set; }

    public string? Roomnumber { get; set; }

    public bool? Isavailable { get; set; }


    public DateTime? AVAILABLEFROM { get; set; }  
    public DateTime? AVAILABLETO { get; set; }
    public string? Bedtype { get; set; }

    public decimal? Price { get; set; }
    public string ? Status { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Hotel? Hotel { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>(); 

    public virtual ICollection<Roomamenity> Roomamenities { get; set; } = new List<Roomamenity>();

    public virtual Roomtype? Roomtype { get; set; }
}
