using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class Card
{
    public int CardId { get; set; }

    public int? CustomerId { get; set; }

    public int? CardTypeId { get; set; }

    public string? CardNumber { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? Status { get; set; }

    public virtual CardType? CardType { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
