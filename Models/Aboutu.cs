using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Aboutu
{
    public decimal Aboutusid { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Imagepath1 { get; set; }

    public string? Imagepath2 { get; set; }
}
