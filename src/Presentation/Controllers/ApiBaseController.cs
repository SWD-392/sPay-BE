using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        private ISender _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();

        protected ObjectResult SPAYResponse<T>(T value)
        {
            return Ok(new ApiResponse<T>(value));
        }

        protected string GetUserId()
        {
            var claim = this.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Name);
            return claim?.Value ?? string.Empty;
        }
    }

    public class ApiResponse<T>
    {
        public ApiResponse(T _result)
        {
            result = _result;
        }
        public T result { get; set; }
    }
}
