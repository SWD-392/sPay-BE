﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPay.BO.DTOs.Admin.Card.Request;
using SPay.BO.DTOs.Admin.Card.Response;
using SPay.BO.Extention.Paginate;
using SPay.Service;
using SPay.Service.Response;

namespace SPay.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CardTypeController : ControllerBase
	{
		private readonly ICardService _service;

		public CardTypeController(ICardService service)
		{
			_service = service;
		}

		/// <summary>
		/// Get all card type
		/// </summary>
		/// <returns></returns>
		[ProducesResponseType(typeof(SPayResponse<IList<CardTypeResponse>>), StatusCodes.Status200OK)]
		[HttpGet]
		public async Task<IActionResult> GetAllcard()
		{
			var response = await _service.GetAllCardTypeAsync();
			return Ok(response);
		}
	}
}
