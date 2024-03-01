using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Deposit
    {
        public Deposit()
        {
            Transactions = new HashSet<Transaction>();
        }

        public string DepositKey { get; set; } = null!;
        public string CardKey { get; set; } = null!;
        public string DepositPackageKey { get; set; } = null!;
        public DateTime Date { get; set; }
        public int? Amount { get; set; }

        public virtual Card CardKeyNavigation { get; set; } = null!;
        public virtual DepositPackage DepositPackageKeyNavigation { get; set; } = null!;
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
