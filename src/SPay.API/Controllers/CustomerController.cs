using Microsoft.AspNetCore.Mvc;
using SPay.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPay.API.Controllers
{
    [Route("api/")]
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
        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _service.GetAllCustomerAsync();
            return Ok(response);
        }

        /// <summary>
        /// Search customers by name
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("customers/search")]
        public string Get(int id)
        {
            return "Test";
        }

        /// <summary>
        /// Create a customer
        /// </summary>
        /// <param name="value"></param>
        [HttpPost ("customer")]
        public void Post([FromBody] string value)
        {

        }

        /// <summary>
        /// Update a customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("customer/{id}")]
        public void Put(int id, [FromBody] string value)
        {


        }

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("customer/{id}")]
        public void Delete(int id)
        {
        }
    }
}
