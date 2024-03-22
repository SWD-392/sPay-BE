using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class WithdrawInformation
    {
        public WithdrawInformation()
        {
            Transactions = new HashSet<Transaction>();
        }

        public string WithdrawKey { get; set; } = null!;
        public string UserKey { get; set; } = null!;
        public decimal? TotalAmount { get; set; }
        public byte Status { get; set; }
        public DateTime InsDate { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
