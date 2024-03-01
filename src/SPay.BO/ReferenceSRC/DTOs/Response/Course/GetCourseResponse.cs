using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.RerferenceSRC.DTOs.Response.Course
{
    public class GetCourseResponse
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool IsAvailable { get; set; }

        public GetCourseResponse()
        {

        }

        public GetCourseResponse(int courseId, string courseName, string description, int categoryId, bool isAvailable)
        {
            CourseId = courseId;
            CourseName = courseName;
            Description = description;
            CategoryId = categoryId;
            IsAvailable = isAvailable;
        }
    }
}
