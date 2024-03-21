using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Membership
    {
        public Membership()
        {
            Orders = new HashSet<Order>();
        }

        public string MembershipKey { get; set; } = null!;
        public string UserKey { get; set; } = null!;
        public string? CardKey { get; set; }
        public bool IsDefaultMembership { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
