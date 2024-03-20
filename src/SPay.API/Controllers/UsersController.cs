using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPay.BO.DTOs.User.Request;
using SPay.BO.DTOs.User.Response;
using SPay.BO.Extention.Paginate;
using SPay.Service;
using SPay.Service.Response;

namespace SPay.API.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _service;

		public UsersController(IUserService service)
		{
			_service = service;
		}

		/// <summary>
		/// Get list user
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(SPayResponse<PaginatedList<UserResponse>>), StatusCodes.Status200OK)]
		[HttpGet()]
		public async Task<IActionResult> GetListGetListCardType([FromQuery] GetListUserRequest request)
		{
			var response = await _service.GetListUserAsync(request);
			if (response.Error == "404")
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Get User by key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[HttpGet("{key}")]
		[ProducesResponseType(typeof(SPayResponse<UserResponse>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetUserByKeyAsync(string key)
		{
			var response = await _service.GetUserByKeyAsync(key);
			if (response.Error == "404")
			{
				return NotFound(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Create a user
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost()]
		public async Task<IActionResult> CreateAUserAsync([FromBody] CreateOrUpdateUserRequest request)
		{
			var response = await _service.CreateUserAsync(request);

			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Update a user
		/// </summary>
		/// <param name="key"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut()]
		public async Task<IActionResult> UpdateAUserAsync(string key, [FromBody] CreateOrUpdateUserRequest request)
		{
			var response = await _service.UpdateUserAsync(key, request);

			if (response.Error != null && response.Error.Equals(SPayResponseHelper.NOT_FOUND))
			{
				return NotFound(response);
			}

			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}

		/// <summary>
		/// Delete a user
		/// </summary>
		/// <param name="key"></param>
		[HttpDelete("{key}")]
		public async Task<IActionResult> DeleteUserAsync(string key)
		{
			var response = await _service.DeleteUserAsync(key);
			if (response.Error != null && response.Error.Equals(SPayResponseHelper.NOT_FOUND))
			{
				return NotFound(response);
			}
			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}
	}
}
