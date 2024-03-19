//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using SPay.BO.DTOs.Admin.Card.Response;
//using SPay.Service.Response;
//using SPay.Service;
//using SPay.BO.DTOs.Admin.Store.Response;

//namespace SPay.API.Controllers
//{
//	[Route("api/[controller]")]
//	[ApiController]
//	public class StoreCategoriesController : ControllerBase
//	{
//		private readonly IStoreService _service;

//		public StoreCategoriesController(IStoreService service)
//		{
//			_service = service;
//		}

//		/// <summary>
//		/// Get all store categories
//		/// </summary>
//		/// <returns></returns>
//		[ProducesResponseType(typeof(SPayResponse<IList<StoreCateResponse>>), StatusCodes.Status200OK)]
//		[HttpGet]
//		public async Task<IActionResult> GetAllStoreCate()
//		{
//			var response = await _service.GetAllStoreCateAsync();
//			return Ok(response);
//		}

//		/// <summary>
//		/// Get store category by key
//		/// </summary>
//		/// <returns></returns>
//		[ProducesResponseType(typeof(SPayResponse<IList<StoreCateResponse>>), StatusCodes.Status200OK)]
//		[HttpGet("{key}")]
//		public async Task<IActionResult> GetStoreCateByKeyAsync(string key)
//		{
//			var response = await _service.GetAllStoreCateAsync();
//			return Ok(response);
//		}
//	}
//}
