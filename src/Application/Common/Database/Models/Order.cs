using System;
using System.Collections.Generic;

namespace Application.Common.Database.Models;

public partial class Order
{
    public int OrderKey { get; set; }

    public int StoreKey { get; set; }

    public int CardKey { get; set; }

    public int? Amount { get; set; }

    public DateOnly? Date { get; set; }

    public bool? Status { get; set; }

    public virtual Card CardKeyNavigation { get; set; } = null!;

    public virtual Store StoreKeyNavigation { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
