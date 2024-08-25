using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarmonyHotles.Models;

public partial class Service
{
    public decimal Servicesid { get; set; }

    public string? Servicename { get; set; }

    public decimal? Serviceprice { get; set; }

    public string? Imagepath { get; set; }

    public string? Status { get; set; }

    [NotMapped]
    public virtual IFormFile? ImageFile { get; set; }
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public virtual ICollection<Hotelservice> Hotelservices { get; set; } = new List<Hotelservice>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}









