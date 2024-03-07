using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Transaction
    {
        public string TransactionKey { get; set; } = null!;
        public string WalletKey { get; set; } = null!;
        public string? DepositKey { get; set; }
        public string? OrderKey { get; set; }
        public string? StoreWithDrawalkey { get; set; }
        public decimal? Value { get; set; }
        public byte Status { get; set; }
        public string? Description { get; set; }

        public virtual Deposit? DepositKeyNavigation { get; set; }
        public virtual Order? OrderKeyNavigation { get; set; }
        public virtual StoreWithdrawal? StoreWithDrawalkeyNavigation { get; set; }
        public virtual Wallet WalletKeyNavigation { get; set; } = null!;
    }
}
