using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Testimonial
{
    public decimal Testimonialid { get; set; }

    public decimal? Customerid { get; set; }

    public string? Message { get; set; }

    public decimal? Rating { get; set; }

    public bool? Isapproved { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
