using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            Stores = new HashSet<Store>();
        }

        public string WalletKey { get; set; } = null!;
        public decimal? Balance { get; set; }
        public string? Description { get; set; }
        public byte Status { get; set; }
        public DateTime InsDate { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}
