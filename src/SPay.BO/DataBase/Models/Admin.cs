using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Admin
    {
        public string AdminKey { get; set; } = null!;
        public string UserKey { get; set; } = null!;
        public string? AdminName { get; set; }
        public string? CreateAt { get; set; }

        public virtual User UserKeyNavigation { get; set; } = null!;
    }
}
