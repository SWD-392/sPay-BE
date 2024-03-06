using Microsoft.AspNetCore.Mvc;
using SPay.Service;

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
        /// Get all transactions
        /// </summary>
        /// <returns></returns>
        [HttpGet("transactions")]
        public async Task<IActionResult> GetAllTransaction()
        {
            var response = await _service.GetAllTransInfoAsync();
            return Ok(response);
        }

        /// <summary>
        /// Search transactions by name
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("transactions/search")]
        public string Get(int id)
        {
            return "value";
        }

		/// <summary>
		/// Create a transaction
		/// </summary>
		/// <param name="value"></param>
		[HttpPost("transaction")]
        public void Post([FromBody] string value)
        {
        }

		/// <summary>
		/// Update a transaction
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("transaction/{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Delete a transaction
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("transaction/{id}")]
        public void Delete(int id)
        {
        }
    }
}
