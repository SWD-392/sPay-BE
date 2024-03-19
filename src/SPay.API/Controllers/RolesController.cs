using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPay.BO.DTOs.Auth.Request;
using SPay.BO.DTOs.Auth.Response;
using SPay.BO.Extention.Paginate;
using SPay.Service;
using SPay.Service.Response;

namespace SPay.API.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
		private readonly IRoleService _service;

		public RolesController(IRoleService service)
		{
			_service = service;
		}

		/// <summary>
		/// Get list role
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<GetListRoleResponse>>), StatusCodes.Status200OK)]
		[HttpGet]
		public async Task<IActionResult> GetGetListRole([FromQuery] GetListRoleRequest request)
		{
			var response = await _service.GetListRoleAsync(request);
			return Ok(response);
		}
	}
}
