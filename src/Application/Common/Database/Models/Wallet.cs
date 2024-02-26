using System;
using System.Collections.Generic;

namespace Application.Common.Database.Models;

public partial class Wallet
{
    public int WalletKey { get; set; }

    public int? WalletTypeKey { get; set; }

    public int CardKey { get; set; }

    public int StoreKey { get; set; }

    public int? Balance { get; set; }

    public string? CreateAt { get; set; }

    public virtual Card CardKeyNavigation { get; set; } = null!;

    public virtual Store StoreKeyNavigation { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();

    public virtual WalletType? WalletTypeKeyNavigation { get; set; }
}
