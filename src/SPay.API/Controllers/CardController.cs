using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SPay.BO.DTOs;
using SPay.BO.DTOs.Admin;
using SPay.BO.DTOs.Admin.Card.Request;
using SPay.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPay.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _service;
        public CardController(ICardService _service)
        {
            this._service = _service;
        }
        /// <summary>
        /// Get all cards
        /// </summary>
        /// <returns></returns>
        [HttpGet("cards")]
        public async Task<IActionResult> GetAllCards([FromQuery] GetAllCardRequest request)
        {
            var response = await _service.GetAllCardsAsync(request);
            return Ok(response);
        }

		/// <summary>
		/// Get card by id
		/// </summary>
		/// <returns></returns>
		[HttpGet("card/{id}")]
		public async Task<IActionResult> GetCardById(string id)
		{
			var response = await _service.GetCardById(id);
			return Ok(response);
		}

		/// <summary>
		/// Search cards by name
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpGet("cards/search")]
        public async Task<IActionResult> SearchCardByName([FromQuery] AdminSearchRequest request)
        {
            var response = await _service.SearchCardAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Create a card
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("card")]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// Update a card
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        [HttpPut("card/{key}")]
        public void Put(string key, [FromBody] string value)
        {

        }

        /// <summary>
        /// Delete a card
        /// </summary>
        /// <param name="key"></param>
        [HttpDelete("card/{key}")]
		public async Task<IActionResult> Delete(string key)
        {
            var response = await _service.DeleteCardAsync(key);

			if (!response.Success)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}
    }
}
