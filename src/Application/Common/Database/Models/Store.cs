using System;
using System.Collections.Generic;

namespace Application.Common.Database.Models;

public partial class Store
{
    public int StoreKey { get; set; }

    public string? Name { get; set; }

    public int CategoryKey { get; set; }

    public string? Location { get; set; }

    public int? Phone { get; set; }

    public bool? Status { get; set; }

    public virtual StoreCategory CategoryKeyNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<StoreOwner> StoreOwners { get; } = new List<StoreOwner>();

    public virtual ICollection<StoreWithdrawal> StoreWithdrawals { get; } = new List<StoreWithdrawal>();

    public virtual ICollection<Wallet> Wallets { get; } = new List<Wallet>();
}
