using SPay.BO.RerferenceSRC.DTOs.Response.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPay.API.Controllers.ReferenceSRC.Constants;
using SPay.BO.RerferenceSRC.DTOs.Request.Authentication;
using SPay.BO.RerferenceSRC.DTOs.Response.Authentication;
using SPay.Service.ReferenceSRC;

namespace SPay.API.Controllers.RerferenceSRC
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AuthenticationController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost(ApiEndPointConstant.Authentication.LoginEndPoint)]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(UnauthorizedObjectResult))]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var loginResponse = await _accountService.Login(loginRequest);
            if (loginResponse == null)
            {
                return Unauthorized(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Error = MessageConstant.LoginMessage.InvalidEmailOrPassword,
                    TimeStamp = DateTime.Now
                });
            }
            if (!loginResponse.Status)
                return Unauthorized(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Error = MessageConstant.LoginMessage.AccountIsUnavailable,
                    TimeStamp = DateTime.Now
                });
            return Ok(loginResponse);
        }

        [AllowAnonymous]
        [HttpPost(ApiEndPointConstant.Authentication.SignUpEndPoint)]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(UnauthorizedObjectResult))]
        public async Task<IActionResult> SignUp(SignUpRequest signUpRequest)
        {
            var response = await _accountService.SignUp(signUpRequest);
            if (response == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.SignUpMessage.EmailHasAlreadyUsed,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(response);
        }
    }
}
