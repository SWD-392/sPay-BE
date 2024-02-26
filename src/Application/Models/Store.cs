using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class Store
{
    public int StoreId { get; set; }

    public string? Name { get; set; }

    public int? CategoryId { get; set; }

    public string? Location { get; set; }

    public int? Phone { get; set; }

    public bool? Status { get; set; }

    public virtual StoreCategory? Category { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<StoreOwner> StoreOwners { get; } = new List<StoreOwner>();

    public virtual ICollection<StoreWithdrawal> StoreWithdrawals { get; } = new List<StoreWithdrawal>();
}
