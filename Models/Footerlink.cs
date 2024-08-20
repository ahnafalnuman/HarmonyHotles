using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Footerlink
{
    public decimal Footerlinkid { get; set; }

    public string? Sectionname { get; set; }

    public string? Linktext { get; set; }

    public string? Linkurl { get; set; }

    public decimal? Displayorder { get; set; }

    public bool? Isactive { get; set; }
}
