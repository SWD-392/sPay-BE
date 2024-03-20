using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPay.BO.DTOs.Admin;
using SPay.BO.Extention.Paginate;
using SPay.Service.Response;
using SPay.Service;
using SPay.BO.DTOs.Store.Request;
using SPay.BO.DTOs.Store.Response;

namespace SPay.API.Controllers
{
    [Route("api/v1/[controller]")]
	[ApiController]
	public class StoresController : ControllerBase
	{
		private readonly IStoreService _service;

		public StoresController(IStoreService service)
		{
			_service = service;
		}

		/// <summary>
		/// Get list store
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<StoreResponse>>), StatusCodes.Status200OK)]
		[HttpGet()]
		public async Task<IActionResult> GetListGetListCardType([FromQuery] GetListStoreRequest request)
		{
			var response = await _service.GetListStoreAsync(request);
			if (response.Error == "404")
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Get store by key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[HttpGet("{key}")]
		[ProducesResponseType(typeof(SPayResponse<StoreResponse>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetStoreByKeyAsync(string key)
		{
			var response = await _service.GetStoreByKeyAsync(key);
			if (response.Error == "404")
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Create a store
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost()]
		public async Task<IActionResult> CreateAStoreAsync([FromBody] CreateOrUpdateStoreRequest request)
		{
			var response = await _service.CreateStoreAsync(request);

			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Update a store
		/// </summary>
		/// <param name="key"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut()]
		public async Task<IActionResult> UpdateAStoreAsync(string key, [FromBody] CreateOrUpdateStoreRequest request)
		{
			var response = await _service.UpdateStoreAsync(key, request);

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
		/// Delete a store
		/// </summary>
		/// <param name="key"></param>
		[HttpDelete("{key}")]
		public async Task<IActionResult> DeleteStoreAsync(string key)
		{
			var response = await _service.DeleteStoreAsync(key);
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
