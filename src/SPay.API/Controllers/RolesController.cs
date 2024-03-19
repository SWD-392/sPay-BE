using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPay.BO.DTOs.Auth.Request;
using SPay.BO.DTOs.Auth.Response;
using SPay.BO.Extention.Paginate;
using SPay.Service;
using SPay.Service.Response;

namespace SPay.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
		private readonly IRoleService _service;

		public RolesController(IRoleService service)
		{
			_service = service;
		}

		/// <summary>
		/// Get all GetListRole
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		//[Authorize]
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<GetListRoleResponse>>), StatusCodes.Status200OK)]
		[HttpGet]
		public async Task<IActionResult> GetGetListRole([FromQuery] GetListRoleRequest request)
		{
			var response = await _service.GetListRoleAsync(request);
			return Ok(response);
		}

		/// <summary>
		/// Get GetListRole by key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[HttpGet("{key}")]
		[ProducesResponseType(typeof(SPayResponse<GetListRoleResponse>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetGetListRoleByKeyAsync(string key)
		{
			//var response = await _service.GetGetListRoleByKeyAsync(key);
			return Ok(/*response*/);
		}

		/// <summary>
		/// Delete a Role
		/// </summary>
		/// <param name="key"></param>
		[HttpDelete("/api/Role/{key}")]
		public async Task<IActionResult> DeleteGetListRoleAsync(string key)
		{
			//var response = await _service.DeleteGetListRoleAsync(key);

			//if (!response.Success)
			//{
			//	return BadRequest(response);
			//}

			return Ok(/*response*/);
		}
	}
}
