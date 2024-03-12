using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class StoreWithdrawal
    {
        public StoreWithdrawal()
        {
            Transactions = new HashSet<Transaction>();
        }

        public string StoreWithdrawalKey { get; set; } = null!;
        public string StoreKey { get; set; } = null!;
        public decimal? Value { get; set; }
        public DateTime Date { get; set; }
        public bool? Status { get; set; }

        public virtual Store StoreKeyNavigation { get; set; } = null!;
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
