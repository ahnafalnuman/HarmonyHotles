using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Header
{
    public decimal Headerid { get; set; }

    public string? Phonenumber { get; set; }

    public string? Socialmedialinks { get; set; }

    public string? Logo { get; set; }

    public bool? Isactive { get; set; }

    public virtual ICollection<Socialmedialink> SocialmedialinksNavigation { get; set; } = new List<Socialmedialink>();
}
