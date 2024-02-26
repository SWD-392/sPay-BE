using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class Card
{
    public int CardKey { get; set; }

    public int CustomerKey { get; set; }

    public int CardTypeKey { get; set; }

    public string? CardNumber { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? Status { get; set; }

    public virtual CardType CardTypeKeyNavigation { get; set; } = null!;

    public virtual Customer CustomerKeyNavigation { get; set; } = null!;

    public virtual ICollection<Deposit> Deposits { get; } = new List<Deposit>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Wallet> Wallets { get; } = new List<Wallet>();
}
