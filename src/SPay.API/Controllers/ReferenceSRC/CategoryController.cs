using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPay.API.Controllers.ReferenceSRC.Constants;
using SPay.BO.RerferenceSRC.DTOs.Request.Category;
using SPay.BO.RerferenceSRC.DTOs.Response.Category;
using SPay.BO.RerferenceSRC.Paginate;
using SPay.Service.ReferenceSRC;

namespace SPay.API.Controllers.RerferenceSRC
{
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet(ApiEndPointConstant.Category.CategoriesEndPoint)]
        [ProducesResponseType(typeof(IPaginate<GetCategoryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories(int page, int size)
        {
            return Ok(await _categoryService.GetAllCategories(page, size));
        }

        [HttpPost(ApiEndPointConstant.Category.CategoriesEndPoint)]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            _categoryService.CreateCategory(request);
            return Ok();
        }

        [HttpPatch(ApiEndPointConstant.Category.CategoriesEndPoint)]
        [ProducesResponseType(typeof(UpdateCategoryResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCategory(int categoryId, UpdateCategoryRequest request)
        {
            UpdateCategoryResponse response = await _categoryService.UpdateCategory(categoryId, request);
            if (response == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(response);
            }
        }
    }
}
