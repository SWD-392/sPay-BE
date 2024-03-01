using SPay.BO.RerferenceSRC.DTOs.Request.Course;
using SPay.BO.RerferenceSRC.DTOs.Response.Course;
using SPay.BO.RerferenceSRC.Paginate;
using SPay.DAO.ReferenceSRC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Repository.ReferenceSRC
{
    public interface ICourseRepository
    {
        public Task<IPaginate<GetCourseResponse>> GetAllCourses(int page, int size);
        public void CreateCourse(CreateCourseRequest createCourseRequest);
        public Task<UpdateCourseResponse> UpdateCourseInformation(int id, UpdateCourseRequest updateCourseRequest);
        public Task<bool> ChangeCourseStatus(int id);
    }
    public class CourseRepository : ICourseRepository
    {
        public async Task<IPaginate<GetCourseResponse>> GetAllCourses(int page, int size) => await CourseDAO.Instance.GetAllCourses(page, size);

        public void CreateCourse(CreateCourseRequest createCourseRequest) => CourseDAO.Instance.CreateCourse(createCourseRequest);

        public async Task<UpdateCourseResponse> UpdateCourseInformation(int id, UpdateCourseRequest updateCourseRequest) => await CourseDAO.Instance.UpdateCourseInformation(id, updateCourseRequest);

        public async Task<bool> ChangeCourseStatus(int id) => await CourseDAO.Instance.ChangeCourseStatus(id);
    }
}
