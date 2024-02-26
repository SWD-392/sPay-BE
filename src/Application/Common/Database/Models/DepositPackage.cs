using System;
using System.Collections.Generic;

namespace Application.Common.Database.Models;

public partial class DepositPackage
{
    public int DepositPackageKey { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? Price { get; set; }

    public virtual ICollection<DepositPackageCardType> DepositPackageCardTypes { get; } = new List<DepositPackageCardType>();

    public virtual ICollection<Deposit> Deposits { get; } = new List<Deposit>();
}
