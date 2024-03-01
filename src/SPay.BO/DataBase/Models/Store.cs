using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Store
    {
        public Store()
        {
            Orders = new HashSet<Order>();
            StoreOwners = new HashSet<StoreOwner>();
            StoreWithdrawals = new HashSet<StoreWithdrawal>();
            Wallets = new HashSet<Wallet>();
        }

        public string StoreKey { get; set; } = null!;
        public string? Name { get; set; }
        public string CategoryKey { get; set; } = null!;
        public string? Location { get; set; }
        public int? Phone { get; set; }
        public bool? Status { get; set; }

        public virtual StoreCategory CategoryKeyNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<StoreOwner> StoreOwners { get; set; }
        public virtual ICollection<StoreWithdrawal> StoreWithdrawals { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
