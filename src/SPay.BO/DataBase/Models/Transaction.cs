using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Transaction
    {
        public string TransactionKey { get; set; } = null!;
        public string? OrderKey { get; set; }
        public string? WithdrawKey { get; set; }
        public byte Status { get; set; }
        public DateTime InsDate { get; set; }

        public virtual Order? OrderKeyNavigation { get; set; }
        public virtual WithdrawInformation? WithdrawKeyNavigation { get; set; }
    }
}
