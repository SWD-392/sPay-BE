using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.RerferenceSRC.DTOs.Request.Course
{
    public class UpdateCourseRequest
    {
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
