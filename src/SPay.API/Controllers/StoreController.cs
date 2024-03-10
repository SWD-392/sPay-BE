using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPay.BO.DTOs.Admin.Card.Response;
using SPay.BO.DTOs.Admin.Store.Request;
using SPay.BO.DTOs.Admin;
using SPay.BO.Extention.Paginate;
using SPay.Service.Response;
using SPay.Service;
using SPay.BO.DTOs.Admin.Store.Response;

namespace SPay.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StoreController : ControllerBase
	{
		private readonly IStoreService _service;
		public StoreController(IStoreService _service)
		{
			this._service = _service;
		}

		/// <summary>
		/// Get all store
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<StoreResponse>>), StatusCodes.Status200OK)]
		[HttpGet]
		public async Task<IActionResult> GetAllStore([FromQuery] GetAllStoreRequest request)
		{
			var response = await _service.GetAllStoreInfoAsync(request);
			return Ok(response);
		}

		/// <summary>
		/// Search stores by name
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<CardResponse>>), StatusCodes.Status200OK)]
		[HttpGet("search")]
		public async Task<IActionResult> SeachStoreByName([FromQuery] AdminSearchRequest request)
		{
			var response = await _service.SearchStoreAsync(request);
			return Ok(response);
		}

		/// <summary>
		/// Get store by key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[HttpGet("{key}")]
		[ProducesResponseType(typeof(SPayResponse<CardResponse>), StatusCodes.Status200OK)]
		public async Task<IActionResult> SeachStoreByName(string key)
		{
			var response = await _service.GetStoreByKeyAsync(key);
			return Ok(response);
		}

		/// <summary>
		/// Create a store
		/// </summary>
		/// <param name="request"></param>
		[HttpPost]
		public async Task<IActionResult> CreateStoreAsync([FromBody] CreateStoreRequest request)
		{
			var response = await _service.CreateStoreAsync(request);
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

			if (!response.Success)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
