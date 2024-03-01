using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.ReferenceSRC.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        [ForeignKey("CourseId")]
        public int CourseId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Course Course { get; set; }
    }
}
