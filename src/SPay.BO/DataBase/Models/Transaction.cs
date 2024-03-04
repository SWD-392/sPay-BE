using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Transaction
    {
        public string TransactionKey { get; set; } = null!;
        public string WalletKey { get; set; } = null!;
        public string DepositKey { get; set; } = null!;
        public string OrderKey { get; set; } = null!;
        public string? StoreWithDrawalkey { get; set; }
        public decimal? Amount { get; set; }
        public bool? Status { get; set; }
        public string? Description { get; set; }

        public virtual Deposit DepositKeyNavigation { get; set; } = null!;
        public virtual Order OrderKeyNavigation { get; set; } = null!;
        public virtual StoreWithdrawal? StoreWithDrawalkeyNavigation { get; set; }
        public virtual Wallet WalletKeyNavigation { get; set; } = null!;
    }
}
