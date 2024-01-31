using Application.Customer.Commands.CreateOrUpdateCustomerCommand;
using Application.Customer.Queries;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class CustomerController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateOrUpdateCustomerCommand customerInput)
        {

            // Gọi handler của MediatR để xử lý tạo hoặc cập nhật khách hàng
            var result = await _mediator.Send(customerInput);
            // Xử lý kết quả từ handler (result)

            // Optionally, you can return the newly created customer or a success message
            return SPAYResponse(result);
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return SPAYResponse(await _mediator.Send(new GetAllCustomerQuery()));
        }
    }
}
