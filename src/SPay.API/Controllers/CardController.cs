//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using SPay.BO.DTOs;
//using SPay.BO.DTOs.Admin;
//using SPay.BO.DTOs.Admin.Card.Request;
//using SPay.BO.DTOs.Admin.Card.Response;
//using SPay.BO.DTOs.Auth.Response;
//using SPay.BO.Extention.Paginate;
//using SPay.Service;
//using SPay.Service.Response;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace SPay.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CardController : ControllerBase
//    {
//		private readonly ICardService _service;
//		public CardController(ICardService _service)
//		{
//			this._service = _service;
//		}

//		/// <summary>
//		/// Get all card
//		/// </summary>
//		/// <param name="request"></param>
//		/// <returns></returns>
//		//[Authorize]
//		[ProducesResponseType(typeof(SPayResponse<PaginatedList<CardResponse>>), StatusCodes.Status200OK)]
//		[HttpGet]
//		public async Task<IActionResult> GetListCard([FromQuery] GetListCardRequest request)
//		{
//			var response = await _service.GetListCardsAsync(request);
//			return Ok(response);
//		}

//		/// <summary>
//		/// Get card by key
//		/// </summary>
//		/// <param name="key"></param>
//		/// <returns></returns>
//		[HttpGet("{key}")]
//		[ProducesResponseType(typeof(SPayResponse<CardResponse>), StatusCodes.Status200OK)]
//		public async Task<IActionResult> GetCardByKeyAsync(string key)
//		{
//			var response = await _service.GetCardByKeyAsync(key);
//			return Ok(response);
//		}

//		/// <summary>
//		/// Create a card
//		/// </summary>
//		/// <param name="request"></param>		
//		[HttpPost]
//		public async Task<IActionResult> CreateCardAsync([FromBody] CreateCardRequest request)
//		{
//			var response = await _service.CreateCardAsync(request);
//			if (!response.Success)
//			{
//				return BadRequest(response);
//			}
//			return Ok(response);
//		}

//		/// <summary>
//		/// Delete a card
//		/// </summary>
//		/// <param name="key"></param>
//		[HttpDelete("{key}")]
//		public async Task<IActionResult> DeleteCardAsync(string key)
//		{
//			var response = await _service.DeleteCardAsync(key);

//			if (!response.Success)
//			{
//				return BadRequest(response);
//			}

//			return Ok(response);
//		}
//	}
//}
