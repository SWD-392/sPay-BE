using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.ReferenceSRC.Models
{
    public class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }
        [Key]
        public int RoleId { get; set; }
        [StringLength(50)]
        public string RoleName { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
