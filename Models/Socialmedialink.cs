using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Socialmedialink
{
    public decimal Socialmediaid { get; set; }

    public string? Platformname { get; set; }

    public string? Iconclass { get; set; }

    public string? Urllink { get; set; }

    public decimal? Headerid { get; set; }

    public decimal? Footerid { get; set; }

    public virtual Footersetting? Footer { get; set; }

    public virtual Header? Header { get; set; }
}
