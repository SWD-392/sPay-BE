using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPay.BO.DTOs.Auth.Response;
using SPay.BO.Extention.Paginate;
using SPay.Service.Response;

namespace SPay.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
		/// <summary>
		/// Get all GetListRole
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		//[Authorize]
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<GetListRoleResponse>>), StatusCodes.Status200OK)]
		[HttpGet]
		public async Task<IActionResult> GetListGetListRole()
		{
			//var response = await _service.GetListGetListRolesAsync(request);
			return Ok(/*response*/);
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
		[HttpDelete("{key}")]
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
