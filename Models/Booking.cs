using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Booking
{
    public decimal Bookingid { get; set; }

    public decimal? Hotelid { get; set; }

    public decimal? Roomid { get; set; }

    public decimal? Eventid { get; set; }

    public decimal? Servicesid { get; set; }

    public decimal? Customerid { get; set; }

    public DateTime? Checkindate { get; set; }

    public DateTime? Checkoutdate { get; set; }

    public decimal? Totalprice { get; set; }

    public string? Bookingstatus { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Room? Room { get; set; }

    public virtual Service? Services { get; set; }
}
