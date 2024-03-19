using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public string RoleKey { get; set; } = null!;
        public string? RoleName { get; set; }
        public string? Description { get; set; }
        public DateTime InsDate { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
