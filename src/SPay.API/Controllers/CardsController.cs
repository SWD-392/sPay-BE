using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SPay.BO.DTOs;
using SPay.BO.DTOs.Admin;
using SPay.BO.DTOs.Auth.Response;
using SPay.BO.DTOs.Card.Request;
using SPay.BO.DTOs.Card.Response;
using SPay.BO.Extention.Paginate;
using SPay.Service;
using SPay.Service.Response;

namespace SPay.API.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class CardsController : ControllerBase
	{
		private readonly ICardService _service;
		public CardsController(ICardService _service)
		{
			this._service = _service;
		}

		/// <summary>
		/// Get list card
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<CardResponse>>), StatusCodes.Status200OK)]
		[HttpGet()]
		public async Task<IActionResult> GetListCard([FromQuery] GetListCardRequest request)
		{
			var response = await _service.GetListCardAsync(request);
			if (response.Error == "404")
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Get card by key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[HttpGet("{key}")]
		[ProducesResponseType(typeof(SPayResponse<CardResponse>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetCardByKeyAsync(string key)
		{
			var response = await _service.GetCardByKeyAsync(key);
			if (response.Error == "404")
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Delete a card
		/// </summary>
		/// <param name="key"></param>
		[HttpDelete("{key}")]
		public async Task<IActionResult> DeleteCardAsync(string key)
		{
			var response = await _service.DeleteCardAsync(key);
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
		/// Create a card
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost()]
		public async Task<IActionResult> CreateACardAsync([FromBody] CreateOrUpdateCardRequest request)
		{
			var response = await _service.CreateCardAsync(request);

			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Update a card
		/// </summary>
		/// <param name="key"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut()]
		public async Task<IActionResult> UpdateACardAsync(string key, [FromBody] CreateOrUpdateCardRequest request)
		{
			var response = await _service.UpdateCardAsync(key, request);

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
