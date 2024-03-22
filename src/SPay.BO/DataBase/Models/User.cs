using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class User
    {
        public User()
        {
            Stores = new HashSet<Store>();
            WithdrawInformations = new HashSet<WithdrawInformation>();
        }

        public string UserKey { get; set; } = null!;
        public string ZaloId { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string RoleKey { get; set; } = null!;
        public byte Status { get; set; }
        public DateTime InsDate { get; set; }

        public virtual Role RoleKeyNavigation { get; set; } = null!;
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<WithdrawInformation> WithdrawInformations { get; set; }
    }
}
