using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Customer
{
    public decimal Customerid { get; set; }

    public string? Fname { get; set; }

    public string Lname { get; set; } = null!;

    public string? Email { get; set; }

    public string? Imagepath { get; set; }

    public string? Phonenumber { get; set; }

    public decimal? Bookingcount { get; set; }

    public bool? Isactive { get; set; }

    public virtual ICollection<Bank> Banks { get; set; } = new List<Bank>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>();

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}
