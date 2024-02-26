using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class StoreWithdrawal
{
    public int WithdrawalId { get; set; }

    public int? StoreId { get; set; }

    public int? Amount { get; set; }

    public DateOnly? Date { get; set; }

    public bool? Status { get; set; }

    public virtual Store? Store { get; set; }
}
