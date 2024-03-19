using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPay.BO.DTOs.CardType.Request;
using SPay.BO.DTOs.CardType.Response;
using SPay.BO.Extention.Paginate;
using SPay.Service;
using SPay.Service.Response;

namespace SPay.API.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class CardTypesController : ControllerBase
	{
		private readonly ICardTypeService _service;

		public CardTypesController(ICardTypeService service)
		{
			_service = service;
		}

		/// <summary>
		/// Get list card type
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<CardTypeResponse>>), StatusCodes.Status200OK)]
		[HttpGet()]
		public async Task<IActionResult> GetListGetListCardType([FromQuery] GetListCardTypeRequest request)
		{
			var response = await _service.GetListCardTypeAsync(request);
			if (response.Error == "404")
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Get card type by key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[HttpGet("{key}")]
		[ProducesResponseType(typeof(SPayResponse<CardTypeResponse>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetCardTypeByKeyAsync(string key)
		{
			var response = await _service.GetCardTypeByKeyAsync(key);
			if (response.Error == "404")
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Delete a card type
		/// </summary>
		/// <param name="key"></param>
		[HttpDelete("{key}")]
		public async Task<IActionResult> DeleteCardTypeAsync(string key)
		{
			var response = await _service.DeleteCardTypeAsync(key);
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
		/// Create a card type
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost()]
		public async Task<IActionResult> CreateACardTypeAsync([FromBody] CreateOrUpdateCardTypeRequest request)
		{
			var response = await _service.CreateCardTypeAsync(request);

			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Update a card type
		/// </summary>
		/// <param name="key"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut()]
		public async Task<IActionResult> UpdateACardTypeAsync(string key, [FromBody] CreateOrUpdateCardTypeRequest request)
		{
			var response = await _service.UpdateCardTypeAsync(key, request);

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
