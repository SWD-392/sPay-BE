using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Cards = new HashSet<Card>();
        }

        public string CustomerKey { get; set; } = null!;
        public string UserKey { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? CreateBy { get; set; }

        public virtual User UserKeyNavigation { get; set; } = null!;
        public virtual ICollection<Card> Cards { get; set; }
    }
}
