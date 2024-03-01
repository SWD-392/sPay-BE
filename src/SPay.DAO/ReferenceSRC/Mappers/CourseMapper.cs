using AutoMapper;
using SPay.BO.ReferenceSRC.Models;
using SPay.BO.RerferenceSRC.DTOs.Request.Course;
using SPay.BO.RerferenceSRC.DTOs.Response.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.DAO.ReferenceSRC.Mappers
{
    public class CourseMapper : Profile
    {
        public CourseMapper()
        {
            CreateMap<CreateCourseRequest, Course>();
            CreateMap<Course, UpdateCourseResponse>();
        }
    }
}
