using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class CardStoreCategory
{
    public int Id { get; set; }

    public int? CardTypeId { get; set; }

    public int? StoreCategoryId { get; set; }

    public virtual CardType? CardType { get; set; }

    public virtual StoreCategory? StoreCategory { get; set; }
}
