﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPay.BO.DTOs.Auth.Request;
using SPay.BO.DTOs.Auth.Response;
using SPay.Repository.Enum;
using SPay.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPay.API.Controllers
{
	[Route("api/v1/auth")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthenticateService _service;

		public AuthenticationController(IAuthenticateService service)
		{
			_service = service;
		}

		[AllowAnonymous]
		[HttpPost("login")]
		[ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(UnauthorizedObjectResult))]
		public async Task<IActionResult> Login(LoginRequest loginRequest)
		{
			var loginResponse = await _service.Login(loginRequest);
			if (loginResponse == null)
			{
				return Unauthorized(new
				{
					StatusCode = StatusCodes.Status401Unauthorized,
					Error = Constant.LoginMessage.InvalidEmailOrPassword,
					TimeStamp = DateTime.Now
				});
			}
			if (loginResponse.Status.Equals(UserStatusEnum.Banned))
				return Unauthorized(new
				{
					StatusCode = StatusCodes.Status401Unauthorized,
					Error = Constant.LoginMessage.AccountIsUnavailable,
					TimeStamp = DateTime.Now
				});
			return Ok(loginResponse);
		}

		[HttpPost("logout")]
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			try
			{
				// Xác thực người dùng và lấy thông tin đăng nhập
				var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
				if (claimsIdentity == null)
				{
					return Unauthorized();
				}

				// Hủy bỏ token của người dùng bằng cách xóa token khỏi danh sách lưu trữ hoặc cơ sở dữ liệu
				return Ok(new { message = "Logout successful" });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Internal server error", error = ex.Message });
			}
		}


		//[AllowAnonymous]
		//[HttpPost("sign-up")]
		//[ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
		//[ProducesErrorResponseType(typeof(UnauthorizedObjectResult))]
		//public async Task<IActionResult> SignUp(SignUpRequest signUpRequest)
		//{
		//	try
		//	{
		//		var response = await _service.SignUp(signUpRequest);
		//		return Ok(response);
		//	}
		//	catch (Exception)
		//	{
		//		return BadRequest(new
		//		{
		//			StatusCode = StatusCodes.Status400BadRequest,
		//			Error = MessageConstant.SignUpMessage.EmailHasAlreadyUsed,
		//			TimeStamp = DateTime.Now
		//		});
		//	}
		//}
	}
}
