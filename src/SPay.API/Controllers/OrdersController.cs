using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPay.BO.DTOs.Order.Request;
using SPay.BO.DTOs.Order.Response;
using SPay.BO.Extention.Paginate;
using SPay.Service;
using SPay.Service.Response;

namespace SPay.API.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderService _service;
		public OrdersController(IOrderService service)
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
		public async Task<IActionResult> GetAllOrder([FromQuery] GetListOrderRequest request)
		{
			var response = await _service.GetAllOrdersAsync(request);
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
	}
}
