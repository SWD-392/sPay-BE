using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class StoreCategory
    {
        public StoreCategory()
        {
            Stores = new HashSet<Store>();
        }

        public string StoreCategoryKey { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public byte Status { get; set; }
        public DateTime InsDate { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}
