using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Bank
{
    public decimal Accountid { get; set; }

    public decimal Cardnumber { get; set; }

    public DateTime? Expirationdate { get; set; }

    public decimal Cvv { get; set; }

    public decimal Balance { get; set; }

    public decimal? Customerid { get; set; }

    public virtual Customer? Customer { get; set; }
}
