using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.ReferenceSRC.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }

        public virtual Account Account { get; set; }
    }
}
