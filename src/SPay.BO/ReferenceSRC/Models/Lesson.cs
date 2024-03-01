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
    public class Lesson
    {
        [Key]
        public int LessonId { get; set; }
        [ForeignKey("CourseId")]
        public int CourseId { get; set; }
        [StringLength(100)]
        public string LessonName { get; set; }
        public string Type { get; set; }
        public string MaterialUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [DefaultValue(false)]
        public bool IsFinished { get; set; }

        public virtual Course Course { get; set; }
    }
}
