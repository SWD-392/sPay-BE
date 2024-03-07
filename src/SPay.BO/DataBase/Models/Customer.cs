using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Wallets = new HashSet<Wallet>();
        }

        public string CustomerKey { get; set; } = null!;
        public string UserKey { get; set; } = null!;
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? CreateBy { get; set; }
        public byte Status { get; set; }

        public virtual User UserKeyNavigation { get; set; } = null!;
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
