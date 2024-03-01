using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.ReferenceSRC.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        [ForeignKey("CourseId")]
        public int CourseId { get; set; }
        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }

        public virtual Course Course { get; set; }
        public virtual Account Account { get; set; }
    }
}
