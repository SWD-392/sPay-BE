using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? StoreId { get; set; }

    public int? CardId { get; set; }

    public int? Amount { get; set; }

    public DateOnly? Date { get; set; }

    public bool? Status { get; set; }

    public virtual Card? Card { get; set; }

    public virtual Store? Store { get; set; }
}
