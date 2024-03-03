using Microsoft.AspNetCore.Mvc;
using SPay.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPay.API.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _service;
        public CardController(ICardService _service)
        {
            this._service = _service;
        }
        // GET: api/<ValuesController>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCards()
        {
            var response = await _service.GetAllCardsAsync();
            return Ok(response);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{name}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
