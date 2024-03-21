using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPay.BO.DTOs.Membership.Request;
using SPay.BO.DTOs.Membership.Response;
using SPay.BO.Extention.Paginate;
using SPay.Service.Response;
using SPay.Service;

namespace SPay.API.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class MembershipsController : ControllerBase
	{
		private readonly IMembershipService _service;
		public MembershipsController(IMembershipService _service)
		{
			this._service = _service;
		}

		/// <summary>
		/// Get list Membership
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<MembershipResponse>>), StatusCodes.Status200OK)]
		[HttpGet()]
		public async Task<IActionResult> GetListMembership([FromQuery] GetListMembershipRequest request)
		{
			var response = await _service.GetListMembershipAsync(request);
			if (response.Error == "404")
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Get Membership by key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[HttpGet("{key}")]
		[ProducesResponseType(typeof(SPayResponse<MembershipResponse>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetMembershipByKeyAsync(string key)
		{
			var response = await _service.GetMembershipByKeyAsync(key);
			if (response.Error == "404")
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Create a Membership
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost()]
		public async Task<IActionResult> CreateAMembershipAsync([FromBody] CreateOrUpdateMembershipRequest request)
		{
			var response = await _service.CreateMembershipAsync(request);

			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}
	}
}
