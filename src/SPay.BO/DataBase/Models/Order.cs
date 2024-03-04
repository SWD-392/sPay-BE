using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Order
    {
        public Order()
        {
            Transactions = new HashSet<Transaction>();
        }

        public string OrderKey { get; set; } = null!;
        public string StoreKey { get; set; } = null!;
        public string CardKey { get; set; } = null!;
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public bool? Status { get; set; }

        public virtual Card CardKeyNavigation { get; set; } = null!;
        public virtual Store StoreKeyNavigation { get; set; } = null!;
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
