using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Footersetting
{
    public decimal Footerid { get; set; }

    public string? Logopath { get; set; }

    public string? Address { get; set; }

    public string? Phonenumber { get; set; }

    public string? Email { get; set; }

    public string? Newslettertitle { get; set; }

    public string? Newslettertext { get; set; }

    public string? Buttonlink { get; set; }

    public string? Copyrighttext { get; set; }

    public virtual ICollection<Socialmedialink> Socialmedialinks { get; set; } = new List<Socialmedialink>();
}
