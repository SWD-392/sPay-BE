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
    public class Course
    {
        public Course()
        {
            Lessons = new HashSet<Lesson>();
            Feedbacks = new HashSet<Feedback>();
            OrderDetails = new HashSet<OrderDetail>();
            Assignments = new HashSet<Assignment>();
            OwnedCourses = new HashSet<OwnedCourse>();
        }

        [Key]
        public int CourseId { get; set; }
        [StringLength(100)]
        public string CourseName { get; set; }
        public string Description { get; set; }
        [DefaultValue(true)]
        public bool IsAvailable { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<OwnedCourse> OwnedCourses { get; set; }
    }
}
