using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPay.Service.Response;
using SPay.Service;
using SPay.Repository;
using SPay.BO.DTOs.CardType.Request;
using SPay.BO.DTOs.CardType.Response;
using SPay.BO.Extention.Paginate;
using SPay.BO.DTOs.Store.Response;
using SPay.BO.DTOs.StoreCategory.Response;
using SPay.BO.DTOs.StoreCategory.Request;

namespace SPay.API.Controllers
{
    [Route("api/v1/[controller]")]
	[ApiController]
	public class StoreCategoriesController : ControllerBase
	{
		private readonly IStoreCategoryService _service;

		public StoreCategoriesController(IStoreCategoryService service)
		{
			_service = service;
		}

		/// <summary>
		/// Get list store category
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<StoreCateResponse>>), StatusCodes.Status200OK)]
		[HttpGet()]
		public async Task<IActionResult> GetListGetListCardType([FromQuery] GetListStoreCateRequest request)
		{
			var response = await _service.GetListStoreCateAsync(request);
			if (response.Error == "404")
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Get store category by key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[HttpGet("{key}")]
		[ProducesResponseType(typeof(SPayResponse<StoreCateResponse>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetStoreCateByKeyAsync(string key)
		{
			var response = await _service.GetStoreCateByKeyAsync(key);
			if (response.Error == "404")
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Create a store category
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost()]
		public async Task<IActionResult> CreateAStoreCateAsync([FromBody] CreateOrUpdateStoreCateRequest request)
		{
			var response = await _service.CreateStoreCateAsync(request);

			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Update a store category
		/// </summary>
		/// <param name="key"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut()]
		public async Task<IActionResult> UpdateAStoreCateAsync(string key, [FromBody] CreateOrUpdateStoreCateRequest request)
		{
			var response = await _service.UpdateStoreCateAsync(key, request);

			if (response.Error != null && response.Error.Equals(SPayResponseHelper.NOT_FOUND))
			{
				return NotFound(response);
			}

			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Delete a store category
		/// </summary>
		/// <param name="key"></param>
		[HttpDelete("{key}")]
		public async Task<IActionResult> DeleteStoreCateAsync(string key)
		{
			var response = await _service.DeleteStoreCateAsync(key);
			if (response.Error != null && response.Error.Equals(SPayResponseHelper.NOT_FOUND))
			{
				return NotFound(response);
			}
			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}
	}
}
