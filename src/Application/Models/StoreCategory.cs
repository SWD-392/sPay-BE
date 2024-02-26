using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class StoreCategory
{
    public int StoreCategoryId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<CardStoreCategory> CardStoreCategories { get; } = new List<CardStoreCategory>();

    public virtual ICollection<Store> Stores { get; } = new List<Store>();
}
