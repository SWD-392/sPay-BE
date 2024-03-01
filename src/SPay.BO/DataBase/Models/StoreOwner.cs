using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class StoreOwner
    {
        public string StoreOwnerKey { get; set; } = null!;
        public string UserKey { get; set; } = null!;
        public string? OwnerName { get; set; }
        public string StoreKey { get; set; } = null!;
        public string? CreateAt { get; set; }

        public virtual Store StoreKeyNavigation { get; set; } = null!;
        public virtual User UserKeyNavigation { get; set; } = null!;
    }
}
