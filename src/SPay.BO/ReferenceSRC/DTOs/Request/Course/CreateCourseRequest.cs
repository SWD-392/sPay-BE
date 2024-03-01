using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.RerferenceSRC.DTOs.Request.Course
{
    public class CreateCourseRequest
    {
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        [DefaultValue(true)]
        public bool IsAvailable { get; set; }
    }
}
