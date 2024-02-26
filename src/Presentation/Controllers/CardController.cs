using Application.Card.Command.CreateOrUpdateCardCommand;
using Application.Card.Queries.GetAllCardQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-card")]
        public async Task<IActionResult> CreateCard([FromBody] CreateOrUpdateCardCommand cardInput)
        {

            // Gọi handler của MediatR để xử lý tạo hoặc cập nhật khách hàng
            var result = await _mediator.Send(cardInput);
            // Xử lý kết quả từ handler (result)

            // Optionally, you can return the newly created card or a success message
            return SPAYResponse(result);
        }




        [HttpGet]
        public async Task<IActionResult> GetAllCard([FromQuery] GetAllCardQuery query)
        {
            var result = await _mediator.Send(query);
            return SPAYResponse(result);
        }
    }
}
