using Microsoft.AspNetCore.Mvc;
using SPay.BO.DTOs.Admin.Transaction.Request;
using SPay.BO.DTOs.Admin.Transaction.Response;
using SPay.BO.DTOs.Admin;
using SPay.BO.Extention.Paginate;
using SPay.Service;
using SPay.Service.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPay.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;
        public TransactionController(ITransactionService _service)
        {
            this._service = _service;
        }

		/// <summary>
		/// Get all Transaction
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<TransactionResponse>>), StatusCodes.Status200OK)]
		[HttpGet]
		public async Task<IActionResult> GetAllTransaction([FromQuery] GetAllTransactionRequest request)
		{
			var response = await _service.GetAllTransactionsAsync(request);
			return Ok();
		}

		/// <summary>
		/// Search cards by name
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<TransactionResponse>>), StatusCodes.Status200OK)]
		[HttpGet("search")]
		public async Task<IActionResult> SearchTransactionAsync([FromQuery] AdminSearchRequest request)
		{
			var response = await _service.SearchTransactionAsync(request);
			return Ok();
		}

		/// <summary>
		/// Get order by key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[HttpGet("{key}")]
		[ProducesResponseType(typeof(SPayResponse<TransactionResponse>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetTransactionByKeyAsync(string key)
		{
			var response = await _service.GetTransactionByKeyAsync(key);
			return Ok(response);
		}

		/// <summary>
		/// Create a card
		/// </summary>
		/// <param name="request"></param>
		[HttpPost]
		public async Task<IActionResult> CreateTransactionAsync([FromBody] CreateTransactionRequest request)
		{
			var response = await _service.CreateTransactionAsync(request);
			if (!response.Success)
			{
				return BadRequest(/*response*/);
			}
			return Ok(/*response*/);
		}

		/// <summary>
		/// Delete a order
		/// </summary>
		/// <param name="key"></param>
		[HttpDelete("{key}")]
		public async Task<IActionResult> DeleteTransactionAsync(string key)
		{
			var response = await _service.DeleteTransactionAsync(key);

			if (!response.Success)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
