using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPay.BO.ReferenceSRC.Models;
using SPay.BO.RerferenceSRC.DTOs.Request.Course;
using SPay.BO.RerferenceSRC.DTOs.Response.Course;
using SPay.BO.RerferenceSRC.Paginate;
using SPay.DAO.ReferenceSRC.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.DAO.ReferenceSRC
{
    public class CourseDAO
    {
        private readonly ITCenterContext _dbContext = null;
        private readonly IMapper _mapper = null;

        private static CourseDAO instance;
        public static CourseDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CourseDAO();
                }
                return instance;
            }
        }

        public CourseDAO()
        {
            if (_dbContext == null)
                _dbContext = new ITCenterContext();
            if (_mapper == null)
                _mapper = new Mapper(new MapperConfiguration(mc => mc.AddProfile(new CourseMapper())).CreateMapper().ConfigurationProvider);
        }

        public async void CreateCourse(CreateCourseRequest newCourse)
        {
            _dbContext.Courses.Add(_mapper.Map<Course>(newCourse));
            await _dbContext.SaveChangesAsync();

        }

        public async Task<IPaginate<GetCourseResponse>> GetAllCourses(int page, int size)
        {
            IPaginate<GetCourseResponse> courseList = await _dbContext.Courses.Select(x => new GetCourseResponse
            {
                CourseId = x.CourseId,
                CourseName = x.CourseName,
                Description = x.Description,
                IsAvailable = x.IsAvailable,
                CategoryId = x.CategoryId
            }).Where(x => x.IsAvailable == true).ToPaginateAsync(page, size, 1);
            return courseList;
        }

        public async Task<bool> ChangeCourseStatus(int courseId)
        {
            Course course = _dbContext.Courses.FirstOrDefault(x => x.CourseId.Equals(courseId));

            if (course != null)
            {
                course.IsAvailable = !course.IsAvailable;
                _dbContext.Courses.Update(course);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<UpdateCourseResponse> UpdateCourseInformation(int courseId, UpdateCourseRequest updateCourseRequest)
        {
            Course course = await _dbContext.Courses.FirstOrDefaultAsync(x => x.CourseId == courseId);

            if (course != null)
            {
                course.CourseName = updateCourseRequest.CourseName;
                course.Description = updateCourseRequest.Description;
                course.CategoryId = updateCourseRequest.CategoryId;
                _dbContext.Courses.Update(course);
                await _dbContext.SaveChangesAsync();

                UpdateCourseResponse response = _mapper.Map<UpdateCourseResponse>(course);
                return response;
            }
            return null;
        }
    }
}
