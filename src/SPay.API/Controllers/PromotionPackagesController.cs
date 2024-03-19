using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SPay.BO.DTOs.Auth.Response;
using SPay.BO.DTOs.PromotionPackage.Request;
using SPay.BO.DTOs.PromotionPackage.Response;
using SPay.BO.Extention.Paginate;
using SPay.Service;
using SPay.Service.Response;

namespace SPay.API.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class PromotionPackagesController : ControllerBase
	{
		private readonly IPromotionPackageService _service;

		public PromotionPackagesController(IPromotionPackageService service)
		{
			_service = service;
		}

		/// <summary>
		/// Get all GetListPromotionPackage
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<PromotionPackageResponse>>), StatusCodes.Status200OK)]
		[HttpGet()]
		public async Task<IActionResult> GetListGetListPromotionPackage([FromQuery]GetListPromotionPackageResquest request)
		{
			var response = await _service.GetListPromotionPackageAsync(request);
			if (response.Error == "404")
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Get GetListPromotionPackage by key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[HttpGet("{key}")]
		[ProducesResponseType(typeof(SPayResponse<PromotionPackageResponse>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetPromotionPackageByKeyAsync(string key)
		{
			var response = await _service.GetPromotionPackageByKeyAsync(key);
			if(response.Error == "404")
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Delete a promotion package
		/// </summary>
		/// <param name="key"></param>
		[HttpDelete("{key}")]
		public async Task<IActionResult> DeletePromotionPackageAsync(string key)
		{
			var response = await _service.DeletePromotionPackageAsync(key);
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
		/// Create a promotion package
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost()]
		public async Task<IActionResult> CreateAPromotionPackageAsync([FromBody] CreateOrUpdatePromotionPackageRequest request)
		{
			var response = await _service.CreatePromotionPackageAsync(request);

			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Update a promotion package
		/// </summary>
		/// <param name="key"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut()]
		public async Task<IActionResult> UpdateAPromotionPackageAsync(string key, [FromBody] CreateOrUpdatePromotionPackageRequest request)
		{
			var response = await _service.UpdatePromotionPackageAsync(key, request);

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
