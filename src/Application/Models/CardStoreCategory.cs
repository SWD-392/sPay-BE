using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class CardStoreCategory
{
    public int CardStoreCategoryKey { get; set; }

    public int CardTypeKey { get; set; }

    public int StoreCategoryKey { get; set; }

    public virtual CardType CardTypeKeyNavigation { get; set; } = null!;

    public virtual StoreCategory StoreCategoryKeyNavigation { get; set; } = null!;
}
