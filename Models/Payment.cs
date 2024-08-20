using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Payment
{
    public decimal Paymentid { get; set; }

    public decimal? Customerid { get; set; }

    public decimal? Bookingid { get; set; }

    public decimal? Eventid { get; set; }

    public decimal? Servicesid { get; set; }

    public decimal Amount { get; set; }

    public string Paymentmethod { get; set; } = null!;

    public DateTime? Paymentdate { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Service? Services { get; set; }
}
