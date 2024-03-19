using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPay.BO.DTOs.Admin;
using SPay.BO.DTOs.Admin.Order.Request;
using SPay.BO.DTOs.Admin.Order.Response;
using SPay.BO.Extention.Paginate;
using SPay.Service;
using SPay.Service.Response;

namespace SPay.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderSController : ControllerBase
	{
		private readonly IOrderService _service;
		public OrderSController(IOrderService service)
		{
			_service = service;
		}
		/// <summary>
		/// Get all Order
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<OrderResponse>>), StatusCodes.Status200OK)]
		[HttpGet]
		public async Task<IActionResult> GetAllOrder([FromQuery] GetAllOrderRequest request)
		{
			var response = await _service.GetAllOrdersAsync(request);
			return Ok(response);
		}

		/// <summary>
		/// Search orders by name
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<OrderResponse>>), StatusCodes.Status200OK)]
		[HttpGet("search")]
		public async Task<IActionResult> SearchOrderAsync([FromQuery] OrderSearchRequest request)
		{
			var response = await _service.SearchOrderAsync(request);
			return Ok(response);
		}

		/// <summary>
		/// Get order by key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[HttpGet("{key}")]
		[ProducesResponseType(typeof(SPayResponse<OrderResponse>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetOrderByKeyAsync(string key)
		{
			var response = await _service.GetOrderByKeyAsync(key);
			return Ok(response);
		}

		/// <summary>
		/// Create a order
		/// </summary>
		/// <param name="request"></param>
		[HttpPost]
		public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest request)
		{
			var response = await _service.CreateOrderAsync(request);
			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Delete a order
		/// </summary>
		/// <param name="key"></param>
		[HttpDelete("{key}")]
		public async Task<IActionResult> DeleteOrderAsync(string key)
		{
			var response = await _service.DeleteOrderAsync(key);

			if (!response.Success)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
