using Microsoft.AspNetCore.Mvc;
using SPay.BO.DTOs;
using SPay.BO.DTOs.Admin;
using SPay.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPay.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _service;
        public StoreController(IStoreService _service)
        {
            this._service = _service;
        }

        /// <summary>
        /// Get all stores
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("stores")]
        public async Task<IActionResult> GetAllStore(PagingRequest request)
        {
            var response = await _service.GetAllStoreInfoAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Search stores by name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("stores/search")]
        public async Task<IActionResult> SeachStoreByName(AdminSearchRequest request)
        {
            var response = await _service.SearchStoreAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Create a store
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("store")]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// Update a store
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("store/{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Delete a store
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("store/{id}")]
        public void Delete(int id)
        {
        }
    }
}
