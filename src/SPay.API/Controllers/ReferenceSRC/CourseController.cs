using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPay.API.Controllers.ReferenceSRC.Constants;
using SPay.BO.RerferenceSRC.DTOs.Request.Course;
using SPay.BO.RerferenceSRC.DTOs.Response.Course;
using SPay.BO.RerferenceSRC.Paginate;
using SPay.Service.ReferenceSRC;

namespace SPay.API.Controllers.RerferenceSRC
{
    //[ApiController]
    //[Authorize]
    //public class CourseController : ControllerBase
    //{
    //    private readonly ICourseService _courseService;

    //    public CourseController(ICourseService courseService)
    //    {
    //        _courseService = courseService;
    //    }

    //    [HttpGet(ApiEndPointConstant.Course.CoursesEndPoint)]
    //    [ProducesResponseType(typeof(IPaginate<GetCourseResponse>), StatusCodes.Status200OK)]
    //    public async Task<IActionResult> GetAllCourses(int page, int size)
    //    {
    //        return Ok(await _courseService.GetAllACourses(page, size));
    //    }

    //    [HttpPost(ApiEndPointConstant.Course.CoursesEndPoint)]
    //    public async Task<IActionResult> CreateCourse(CreateCourseRequest createCourseRequest)
    //    {
    //        _courseService.CreateCourse(createCourseRequest);
    //        return Ok();
    //    }

    //    [HttpPatch(ApiEndPointConstant.Course.CourseEndPoint)]
    //    [ProducesResponseType(typeof(UpdateCourseResponse), StatusCodes.Status200OK)]
    //    public async Task<IActionResult> UpdateCourseInformation(int id, UpdateCourseRequest updateCourseRequest)
    //    {
    //        UpdateCourseResponse response = await _courseService.UpdateCourseInformation(id, updateCourseRequest);

    //        if (response != null)
    //        {
    //            return Ok(response);
    //        }
    //        else
    //        {
    //            return BadRequest();
    //        }
    //    }

    //    [HttpPatch(ApiEndPointConstant.Course.CourseStatusEndPoint)]
    //    public async Task<IActionResult> ChangeCourseStatus(int id)
    //    {
    //        bool result = await _courseService.ChangeCourseStatus(id);
    //        if (result)
    //        {
    //            return Ok(result);
    //        }
    //        else
    //        {
    //            return BadRequest();
    //        }
    //    }
    //}
}
