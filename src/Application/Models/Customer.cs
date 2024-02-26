using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int? UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Card> Cards { get; } = new List<Card>();

    public virtual User? User { get; set; }
}
