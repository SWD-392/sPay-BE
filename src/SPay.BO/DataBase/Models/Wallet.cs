using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            Transactions = new HashSet<Transaction>();
        }

        public string WalletKey { get; set; } = null!;
        public string? WalletTypeKey { get; set; }
        public string CardKey { get; set; } = null!;
        public string StoreKey { get; set; } = null!;
        public int? Balance { get; set; }
        public string? CreateAt { get; set; }

        public virtual Card CardKeyNavigation { get; set; } = null!;
        public virtual Store StoreKeyNavigation { get; set; } = null!;
        public virtual WalletType? WalletTypeKeyNavigation { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
