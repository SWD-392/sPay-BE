﻿using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Order
    {
        public Order()
        {
            Transactions = new HashSet<Transaction>();
        }

        public string OrderKey { get; set; } = null!;
        public string MembershipKey { get; set; } = null!;
        public string StoreKey { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? TotalAmount { get; set; }
        public byte Status { get; set; }
        public DateTime InsDate { get; set; }

        public virtual Membership MembershipKeyNavigation { get; set; } = null!;
        public virtual Store StoreKeyNavigation { get; set; } = null!;
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
