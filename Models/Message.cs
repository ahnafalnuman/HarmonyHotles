using System;
using System.Collections.Generic;

namespace HarmonyHotles.Models;

public partial class Message
{
    public decimal Messageid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Subject { get; set; }

    public string Message1 { get; set; } = null!;

    public DateTime? Sentat { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
