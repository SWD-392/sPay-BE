using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class StoreCategory
    {
        public StoreCategory()
        {
            CardStoreCategories = new HashSet<CardStoreCategory>();
            Stores = new HashSet<Store>();
        }

        public string StoreCategoryKey { get; set; } = null!;
        public string? Name { get; set; }
        public byte Status { get; set; }

        public virtual ICollection<CardStoreCategory> CardStoreCategories { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
