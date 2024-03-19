using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Store
    {
        public Store()
        {
            Orders = new HashSet<Order>();
        }

        public string StoreKey { get; set; } = null!;
        public string UserKey { get; set; } = null!;
        public string StoreName { get; set; } = null!;
        public string? Description { get; set; }
        public string StoreCateKey { get; set; } = null!;
        public string WalletKey { get; set; } = null!;
        public byte Status { get; set; }
        public DateTime InsDate { get; set; }

        public virtual User UserKeyNavigation { get; set; } = null!;
        public virtual Wallet WalletKeyNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
