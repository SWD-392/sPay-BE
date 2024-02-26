using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class DepositPackageCardType
{
    public int DepositPackageCardTypeKey { get; set; }

    public int DepositPackageKey { get; set; }

    public int CardTypeKey { get; set; }

    public virtual CardType CardTypeKeyNavigation { get; set; } = null!;

    public virtual DepositPackage DepositPackageKeyNavigation { get; set; } = null!;
}
