using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SPay.BO.DTOs.Admin;
using SPay.BO.DTOs.Admin.Card.Response;
using SPay.BO.DTOs.Admin.Customer.RequestModel;
using SPay.BO.DTOs.Admin.Customer.ResponseModel;
using SPay.BO.Extention.Paginate;
using SPay.Service;
using SPay.Service.Response;

namespace SPay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        public CustomerController(ICustomerService _service)
        {
            this._service = _service;
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<CustomerResponse>>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllCustomers([FromQuery]GetAllCustomerRequest request)
        {
            var response = await _service.GetAllCustomerAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Search customers by name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("search")]
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<CustomerResponse>>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetCustomersByNameContains([FromQuery]AdminSearchRequest request)
        {
			var response = await _service.SearchCustomerAsync(request);
			return Ok(response);
		}

		/// <summary>
		/// Get customer
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(SPayResponse<CardResponse>), StatusCodes.Status200OK)]
		[HttpGet("{key}")]
		public async Task<IActionResult> GetCardById(string key)
		{
			var response = await _service.GetCustomerByKey(key);
			return Ok(response);
		}

		/// <summary>
		/// Create a customer
		/// </summary>
		/// <param name="request"></param>
		[HttpPost]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerRequest request)
        {
			var response = await _service.CreateCustomerAsync(request);
			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}

        ///// <summary>
        ///// Update a customer
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="value"></param>
        //[HttpPut("{key}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="key"></param>
        [HttpDelete("{key}")]
		public async Task<IActionResult> DeleteCustomerAsync(string key)
		{
			var response = await _service.DeleteCustomerAsync(key);

			if (!response.Success)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
