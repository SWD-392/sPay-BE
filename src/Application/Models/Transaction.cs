using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class Transaction
{
    public int TransactionKey { get; set; }

    public int WalletKey { get; set; }

    public int DepositKey { get; set; }

    public int OrderKey { get; set; }

    public int? StoreWithDrawalkey { get; set; }

    public int? Amount { get; set; }

    public bool? Status { get; set; }

    public string? Description { get; set; }

    public virtual Deposit DepositKeyNavigation { get; set; } = null!;

    public virtual Order OrderKeyNavigation { get; set; } = null!;

    public virtual StoreWithdrawal? StoreWithDrawalkeyNavigation { get; set; }

    public virtual Wallet WalletKeyNavigation { get; set; } = null!;
}
