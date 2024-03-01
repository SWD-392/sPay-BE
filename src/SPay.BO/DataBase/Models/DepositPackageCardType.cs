using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class DepositPackageCardType
    {
        public string DepositPackageCardTypeKey { get; set; } = null!;
        public string DepositPackageKey { get; set; } = null!;
        public string CardTypeKey { get; set; } = null!;

        public virtual CardType CardTypeKeyNavigation { get; set; } = null!;
        public virtual DepositPackage DepositPackageKeyNavigation { get; set; } = null!;
    }
}
