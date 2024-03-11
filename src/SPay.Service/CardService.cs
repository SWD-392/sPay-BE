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
		Task<SPayResponse<CardResponse>> GetCardByKeyAsync(string id);
		Task<SPayResponse<PaginatedList<CardResponse>>> SearchCardAsync(AdminSearchRequest request);
		Task<SPayResponse<bool>> DeleteCardAsync(string key);
		Task<SPayResponse<bool>> CreateCardAsync(CreateCardRequest request);
		Task<int> CountCardByUserKey(string key);
		Task<SPayResponse<IList<CardResponse>>> GetListCardKeyByCustomerKey(string customerKey);

		#region CardType
		Task<SPayResponse<IList<CardTypeResponse>>> GetAllCardTypeAsync();
		Task<SPayResponse<IList<CardTypeResponse>>> GetCardTypeByStoreCateKeyAsync(string storeCateKey); 
		#endregion
	}
	public class CardService : ICardService
	{
		private readonly ICardRepository _cardRepo;
		private readonly IMapper _mapper;
		private readonly IWalletService _walletService;
		public CardService(ICardRepository _cardRepo, IMapper mapper, IWalletService _walletService)
		{
			this._cardRepo = _cardRepo;
			this._mapper = mapper;
			this._walletService = _walletService;
		}

		public async Task<int> CountCardByUserKey(string userKey)
		{
			var listCardKey = await _walletService.GetListCardByUserKeyAsync(userKey);
			return listCardKey.Count;
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
					return response;
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
				response.Message = "Create a card successfully";
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
				var existedCard = await _cardRepo.GetCardByKeyAsync(key);
				if (existedCard == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Cannot find card to delete!");
					return response;
				}
				var success = await _cardRepo.DeleteCardAsync(existedCard);
				if (success == false)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = success;
				response.Success = true;
				response.Message = "Card delete successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
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
					SPayResponseHelper.SetErrorResponse(response, "No record in database");
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
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<IList<CardTypeResponse>>> GetAllCardTypeAsync()
		{
			var response = new SPayResponse<IList<CardTypeResponse>>();
			try
			{
				var types = await _cardRepo.GetAllCardTypeAsync();
				if (types == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Card type has no row in database.");
					return response;
				}
				var typeRes = _mapper.Map<IList<CardTypeResponse>>(types);
				response.Data = typeRes;
				response.Success = true;
				response.Message = "Get card type successfully";
				return response;
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<IList<CardTypeResponse>>> GetCardTypeByStoreCateKeyAsync(string storeCateKey)
		{
			var response = new SPayResponse<IList<CardTypeResponse>>();
			try
			{
				var types = await _cardRepo.GetCardTypeByStoreCateKeyAsync(storeCateKey);
				if (types == null)
				{
					SPayResponseHelper.SetErrorResponse(response, $"Card type suitable for storeCateKey:{storeCateKey} has no row in database.");
					return response;
				}
				var typeRes = _mapper.Map<IList<CardTypeResponse>>(types);
				response.Data = typeRes;
				response.Success = true;
				response.Message = $"Get card type suitable for storeCateKey:{storeCateKey} successfully";
				return response;

			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<CardResponse>> GetCardByKeyAsync(string key)
		{
			SPayResponse<CardResponse> response = new SPayResponse<CardResponse>();
			var card = await _cardRepo.GetCardByKeyAsync(key);
			try
			{
				if (card == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Card not found!");
					return response;
				}
				var cardRes = _mapper.Map<CardResponse>(card);

				response.Data = cardRes;
				response.Success = true;
				response.Message = $"Get Card key = {cardRes.CardKey} successfully!";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<IList<CardResponse>>> GetListCardKeyByCustomerKey(string customerKey)
		{
			var response = new SPayResponse<IList<CardResponse>>();
			try
			{
				var cards = await _cardRepo.GetListCardByCustomerKey(customerKey);
				if (cards.Count <= 0)
				{
					SPayResponseHelper.SetErrorResponse(response, $"The customer key {customerKey} has no cards.");
					return response;
				}
				var cardRes = _mapper.Map<IList<CardResponse>>(cards);
				response.Data = cardRes;
				response.Success = true;
				response.Message = $"Get card of customer {customerKey} successfully";
				return response;

			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
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
					SPayResponseHelper.SetErrorResponse(response, "Key word name must not empty or null!");
					return response;
				}
				var cards = await _cardRepo.SearchCardByNameAsync(keyWord);
				if (cards.Count <= 0)
				{
					SPayResponseHelper.SetErrorResponse(response, $"Not found the card with keyword {keyWord}");
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
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}
	}
}
