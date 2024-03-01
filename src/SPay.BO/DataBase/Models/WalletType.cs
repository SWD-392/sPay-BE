using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class WalletType
    {
        public WalletType()
        {
            Wallets = new HashSet<Wallet>();
        }

        public string WalletTypeKey { get; set; } = null!;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Mode { get; set; }

        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
