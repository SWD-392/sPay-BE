using System;
using System.Collections.Generic;

namespace Application.Common.Database.Models;

public partial class Customer
{
    public int CustomerKey { get; set; }

    public int? UserKey { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? CreateBy { get; set; }

    public virtual ICollection<Card> Cards { get; } = new List<Card>();

    public virtual User? UserKeyNavigation { get; set; }
}
