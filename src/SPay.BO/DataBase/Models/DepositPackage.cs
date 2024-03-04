using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class DepositPackage
    {
        public DepositPackage()
        {
            DepositPackageCardTypes = new HashSet<DepositPackageCardType>();
            Deposits = new HashSet<Deposit>();
        }

        public string DepositPackageKey { get; set; } = null!;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<DepositPackageCardType> DepositPackageCardTypes { get; set; }
        public virtual ICollection<Deposit> Deposits { get; set; }
    }
}
