using SPay.BO.RerferenceSRC.DTOs.Request.Course;
using SPay.BO.RerferenceSRC.DTOs.Response.Course;
using SPay.BO.RerferenceSRC.Paginate;
using SPay.Repository.ReferenceSRC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Service.ReferenceSRC
{
    public interface ICourseService
    {
        public Task<IPaginate<GetCourseResponse>> GetAllACourses(int page, int size);
        public void CreateCourse(CreateCourseRequest createCourseRequest);
        public Task<UpdateCourseResponse> UpdateCourseInformation(int id, UpdateCourseRequest updateCourseRequest);
        public Task<bool> ChangeCourseStatus(int id);
    }

    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository = null;
        public CourseService()
        {
            if (_courseRepository == null)
                _courseRepository = new CourseRepository();
        }
        public async Task<bool> ChangeCourseStatus(int id) => await _courseRepository.ChangeCourseStatus(id);

        public async void CreateCourse(CreateCourseRequest createCourseRequest) => _courseRepository.CreateCourse(createCourseRequest);

        public async Task<IPaginate<GetCourseResponse>> GetAllACourses(int page, int size) => await _courseRepository.GetAllCourses(page, size);

        public async Task<UpdateCourseResponse> UpdateCourseInformation(int id, UpdateCourseRequest updateCourseRequest) => await _courseRepository.UpdateCourseInformation(id, updateCourseRequest);
    }
}
