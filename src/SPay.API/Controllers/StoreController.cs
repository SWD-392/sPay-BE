using Microsoft.AspNetCore.Mvc;
using SPay.BO.DTOs;
using SPay.BO.DTOs.Admin;
using SPay.BO.DTOs.Admin.Store.Request;
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
        /// Get all store
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("stores")]
        public async Task<IActionResult> GetAllStore([FromQuery] GetAllStoreRequest request)
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
        public async Task<IActionResult> SeachStoreByName([FromQuery] AdminSearchRequest request)
        {
            var response = await _service.SearchStoreAsync(request);
            return Ok(response);
        }

		/// <summary>
		/// Get store by key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[HttpGet("stores/{key}")]
		public async Task<IActionResult> SeachStoreByName(string key)
		{
			var response = await _service.GetStoreByKeyAsync(key);
			return Ok(response);
		}

		/// <summary>
		/// Create a store
		/// </summary>
		/// <param name="value"></param>
		//[HttpPost("store")]
  //      public async Task<IActionResult> Post([FromBody] string value)
  //      {
  //      }

  //      /// <summary>
  //      /// Update a store
  //      /// </summary>
  //      /// <param name="id"></param>
  //      /// <param name="value"></param>
  //      [HttpPut("store/{key}")]
  //      public async Task<IActionResult> Put(string key, [FromBody] string value)
  //      {
  //      }

        /// <summary>
        /// Delete a store
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("store/{key}")]
        public async Task<IActionResult> Delete(string key)
        {
			var response = await _service.DeleteStoreAsync(key);

			if (!response.Success)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}
    }
}
