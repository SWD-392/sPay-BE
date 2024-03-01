using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.ReferenceSRC.Models
{
    public class OwnedCourse
    {
        [Key]
        public int OwnedCourseId { get; set; }
        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        [ForeignKey("CourseId")]
        public int CourseId { get; set; }
        public bool IsOwned { get; set; }
        public bool IsFinished { get; set; }
        public DateTime FinishedDate { get; set; }

        public virtual Account Account { get; set; }
        public virtual Course Course { get; set; }
    }
}
