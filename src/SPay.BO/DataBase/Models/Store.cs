using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Store
    {
        public Store()
        {
            Orders = new HashSet<Order>();
            StoreWithdrawals = new HashSet<StoreWithdrawal>();
            Wallets = new HashSet<Wallet>();
        }

        public string StoreKey { get; set; } = null!;
        public string? Name { get; set; }
        public string CategoryKey { get; set; } = null!;
        public string? Phone { get; set; }
        public byte Status { get; set; }
        public string? Description { get; set; }
        public string UserKey { get; set; } = null!;

        public virtual StoreCategory CategoryKeyNavigation { get; set; } = null!;
        public virtual User UserKeyNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<StoreWithdrawal> StoreWithdrawals { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
