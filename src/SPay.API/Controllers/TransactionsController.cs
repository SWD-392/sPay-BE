using Microsoft.AspNetCore.Mvc;
using SPay.BO.Extention.Paginate;
using SPay.Service;
using SPay.Service.Response;
using SPay.BO.DTOs.Transaction.Request;
using SPay.BO.DTOs.Transaction.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPay.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _service;
        public TransactionsController(ITransactionService _service)
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
		public async Task<IActionResult> GetAllTransaction([FromQuery] GetListTransactionRequest request)
		{
			var response = await _service.GetAllTransactionsAsync(request);
			if (response.Error == "404")
			{
				return NotFound(response);
			}
			return Ok(response);
		}
	}
}
