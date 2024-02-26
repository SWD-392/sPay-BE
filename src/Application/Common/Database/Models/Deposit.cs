using System;
using System.Collections.Generic;

namespace Application.Common.Database.Models;

public partial class Deposit
{
    public int DepositKey { get; set; }

    public int CardKey { get; set; }

    public int DepositPackageKey { get; set; }

    public DateOnly Date { get; set; }

    public int? Amount { get; set; }

    public virtual Card CardKeyNavigation { get; set; } = null!;

    public virtual DepositPackage DepositPackageKeyNavigation { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
