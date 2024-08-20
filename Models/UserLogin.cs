using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class UserLogin
{
    public decimal UlId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public decimal? Roleid { get; set; }

    public decimal? Customerid { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Role? Role { get; set; }
}
