using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
//using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs;
using SPay.BO.DTOs.Admin;
using SPay.BO.DTOs.Admin.Card.Request;
using SPay.BO.DTOs.Admin.Card.Response;
using SPay.BO.Extention.Paginate;
using SPay.Repository;
using SPay.Repository.Enum;
using SPay.Service.Response;
using SPay.Service.Utils;

namespace SPay.Service
{
	public interface ICardService
	{
		Task<SPayResponse<PaginatedList<CardResponse>>> GetAllCardsAsync(GetAllCardRequest request);
		Task<SPayResponse<CardResponse>> GetCardById(string id);
		Task<SPayResponse<PaginatedList<CardResponse>>> SearchCardAsync(AdminSearchRequest request);
		Task<SPayResponse<bool>> DeleteCardAsync(string key);
		Task<SPayResponse<bool>> CreateCardAsync(CreateCardRequest request);

	}
	public class CardService : ICardService
	{
		private readonly ICardRepository _cardRepo;
		private readonly IMapper _mapper;
		public CardService(ICardRepository _cardRepo, IMapper mapper)
		{
			this._cardRepo = _cardRepo;
			this._mapper = mapper;
		}

		public async Task<SPayResponse<bool>> CreateCardAsync(CreateCardRequest request)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				if (request == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Request model is null!");
					return response;
				}
				var createCardInfo = _mapper.Map<Card>(request);
				if(createCardInfo == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
				}
				createCardInfo.CardKey = string.Format("{0}{1}", PrefixKeyConstant.CARD, Guid.NewGuid().ToString().ToUpper());
				createCardInfo.CreatedAt = DateTimeHelper.GetDateTimeNow();
				createCardInfo.Status = (byte)CardStatusEnum.Available;
				var discountAmount = createCardInfo.MoneyValue.Value * (createCardInfo.DiscountPercentage.Value / 100.0m);
				createCardInfo.Price = createCardInfo.MoneyValue.Value - discountAmount;
				if (!await _cardRepo.CreateCardAsync(createCardInfo))
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = true;
				response.Success = true;
				response.Message = "Card delete successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<bool>> DeleteCardAsync(string key)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				var existedCard = await _cardRepo.GetCardByIdAsync(key);
				if (existedCard == null)
				{
					response.Success = false;
					response.Message = "Card not found.";
					return response;
				}
				var success = await _cardRepo.DeleteCardAsync(existedCard);
				if (success == false)
				{
					response.Success = false;
					response.Message = "Something wrong!";
				}
				response.Data = success;
				response.Success = true;
				response.Message = "Card delete successfully";
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			return response;
		}

		public async Task<SPayResponse<PaginatedList<CardResponse>>> GetAllCardsAsync(GetAllCardRequest request)
		{
			SPayResponse<PaginatedList<CardResponse>> response = new SPayResponse<PaginatedList<CardResponse>>();

			try
			{
				var cards = await _cardRepo.GetAllAsync();
				if (cards == null)
				{
					response.Success = false;
					response.Message = "The result of get all card from repository is null";
					return response;
				}
				var cardsRes = _mapper.Map<IList<CardResponse>>(cards);
				var count = 0;
				foreach (var item in cardsRes)
				{
					item.No = ++count;
				}
				response.Data = await cardsRes.ToPaginateAsync(request);
				response.Success = true;
				response.Message = "Get all card successfully";
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			return response;
		}

		public async Task<SPayResponse<CardResponse>> GetCardById(string key)
		{
			SPayResponse<CardResponse> response = new SPayResponse<CardResponse>();
			var card = await _cardRepo.GetCardByIdAsync(key);
			try
			{
				if (card == null)
				{
					response.Success = false;
					response.Message = "Card not found";
					return response;
				}
				var cardRes = _mapper.Map<CardResponse>(card);

				response.Data = cardRes;
				response.Success = true;
				response.Message = $"Get Card key = {cardRes.CardKey} successfully";
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			return response;
		}

		public async Task<SPayResponse<PaginatedList<CardResponse>>> SearchCardAsync(AdminSearchRequest request)
		{
			SPayResponse<PaginatedList<CardResponse>> response = new SPayResponse<PaginatedList<CardResponse>>();
			try
			{
				var keyWord = request.Keyword.Trim();
				if (string.IsNullOrEmpty(keyWord))
				{
					response.Success = false;
					response.Message = "The keyword is null or empty";
					return response;
				}
				var cards = await _cardRepo.SearchCardByNameAsync(keyWord);
				if (cards.Count <= 0)
				{
					response.Success = false;
					response.Message = $"Not found the card with keyword {keyWord}";
					return response;
				}
				var cardsRes = _mapper.Map<IList<CardResponse>>(cards);
				var count = 0;
				foreach (var item in cardsRes)
				{
					item.No = ++count;
				}
				response.Data = await cardsRes.ToPaginateAsync(request);
				response.Success = true;
				response.Message = "Search cards successfully";
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			return response;
		}
	}
}
