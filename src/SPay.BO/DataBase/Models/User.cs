using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class User
    {
        public User()
        {
            Admins = new HashSet<Admin>();
            Customers = new HashSet<Customer>();
            Stores = new HashSet<Store>();
        }

        public string UserKey { get; set; } = null!;
        public string? Username { get; set; }
        public string? Password { get; set; }
        public byte Role { get; set; }
        public byte Status { get; set; }
        public string? Fullname { get; set; }
        public DateTime? InsDate { get; set; }

        public virtual ICollection<Admin> Admins { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
