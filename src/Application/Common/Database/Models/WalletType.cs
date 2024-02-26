using System;
using System.Collections.Generic;

namespace Application.Common.Database.Models;

public partial class WalletType
{
    public int WalletTypeKey { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? Mode { get; set; }

    public virtual ICollection<Wallet> Wallets { get; } = new List<Wallet>();
}
