using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Notification
{
    public decimal Notificationid { get; set; }

    public decimal? Testimonialid { get; set; }

    public string? Message { get; set; }

    public bool? Isread { get; set; }

    public decimal? Messageid { get; set; }

    public decimal? Bookingid { get; set; }

    public decimal? Customerid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Message? MessageNavigation { get; set; }

    public virtual Testimonial? Testimonial { get; set; }
}
