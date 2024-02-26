using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class CardType
{
    public int CardTypeId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<CardStoreCategory> CardStoreCategories { get; } = new List<CardStoreCategory>();

    public virtual ICollection<Card> Cards { get; } = new List<Card>();
}
