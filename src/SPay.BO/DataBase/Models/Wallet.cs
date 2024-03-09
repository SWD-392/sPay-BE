using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            Stores = new HashSet<Store>();
            Transactions = new HashSet<Transaction>();
        }

        public string WalletKey { get; set; } = null!;
        public string? WalletTypeKey { get; set; }
        public string? CardKey { get; set; }
        public decimal? Balance { get; set; }
        public DateTime? CreateAt { get; set; }
        public byte? Status { get; set; }
        public string? StoreKey { get; set; }
        public string? CustomerKey { get; set; }

        public virtual Card? CardKeyNavigation { get; set; }
        public virtual Customer? CustomerKeyNavigation { get; set; }
        public virtual Store? StoreKeyNavigation { get; set; }
        public virtual WalletType? WalletTypeKeyNavigation { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
