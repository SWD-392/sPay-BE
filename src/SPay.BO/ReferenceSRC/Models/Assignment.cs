using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.ReferenceSRC.Models
{
    public class Assignment
    {
        public Assignment()
        {
            LearnerAssignments = new HashSet<LearnerAssignment>();
        }

        [Key]
        public int AssignmentId { get; set; }
        [DefaultValue(100)]
        public string AssignmentTitle { get; set; }
        public string Question { get; set; }
        public DateTime Deadline { get; set; }
        public string Type { get; set; }
        [ForeignKey("CourseId")]
        public int CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime AssignmentDuration { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<LearnerAssignment> LearnerAssignments { get; set; }
    }
}
