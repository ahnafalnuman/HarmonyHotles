using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Contactinfo
{
    public decimal Contactinfoid { get; set; }

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? State { get; set; }

    public string? Zipcode { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Workinghours { get; set; }

    public string? Imagepath { get; set; }
}
