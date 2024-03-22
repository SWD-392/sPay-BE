//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using SPay.BO.DTOs.WithdrawInfo.Request;
//using SPay.BO.DTOs.WithdrawInfo.Response;
//using SPay.BO.Extention.Paginate;
//using SPay.Service.Response;
//using SPay.Service;
//using SPay.BO.DTOs.WithdrawInfomation.Request;
//using SPay.BO.DTOs.WithdrawInfomation.Response;

//namespace SPay.API.Controllers
//{
//	[Route("api/v1/[controller]")]
//	[ApiController]
//	public class WithdrawInfosController : ControllerBase
//	{
//		private readonly IWithdrawInfoService _service;
//		public WithdrawInfosController(IWithdrawInfoService service)
//		{
//			_service = service;
//		}
//		/// <summary>
//		/// Get all WithdrawInfo
//		/// </summary>
//		/// <param name="request"></param>
//		/// <returns></returns>
//		[ProducesResponseType(typeof(SPayResponse<PaginatedList<WithdrawInfomationResponse>>), StatusCodes.Status200OK)]
//		[HttpGet]
//		public async Task<IActionResult> GetAllWithdrawInfo([FromQuery] GetListWithdrawInfomationRequest request)
//		{
//			var response = await _service.GetAllWithdrawInfosAsync(request);
//			return Ok(response);
//		}

//		/// <summary>
//		/// Get order by key
//		/// </summary>
//		/// <param name="key"></param>
//		/// <returns></returns>
//		[HttpGet("{key}")]
//		[ProducesResponseType(typeof(SPayResponse<WithdrawInfomationResponse>), StatusCodes.Status200OK)]
//		public async Task<IActionResult> GetWithdrawInfoByKeyAsync(string key)
//		{
//			var response = await _service.GetWithdrawInfoByKeyAsync(key);
//			return Ok(response);
//		}

//		/// <summary>
//		/// Create a order
//		/// </summary>
//		/// <param name="request"></param>
//		[HttpPost]
//		public async Task<IActionResult> CreateWithdrawInfoAsync([FromBody] CreateWithdrawInfomationRequest request)
//		{
//			var response = await _service.CreateWithdrawInfoAsync(request);
//			if (!response.Success)
//			{
//				return BadRequest(response);
//			}
//			return Ok(response);
//		}
//	}
//}
