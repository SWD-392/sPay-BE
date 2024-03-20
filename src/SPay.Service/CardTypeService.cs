using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Card.Request;
using SPay.BO.DTOs.CardType.Request;
using SPay.BO.DTOs.CardType.Response;
using SPay.BO.DTOs.PromotionPackage.Response;
using SPay.BO.Extention.Paginate;
using SPay.Repository;
using SPay.Repository.Enum;
using SPay.Service.Response;
using SPay.Service.Utils;

namespace SPay.Service
{
	public interface ICardTypeService
	{
		Task<SPayResponse<PaginatedList<CardTypeResponse>>> GetListCardTypeAsync(GetListCardTypeRequest request);
		Task<SPayResponse<CardTypeResponse>> GetCardTypeByKeyAsync(string key);
		Task<SPayResponse<bool>> DeleteCardTypeAsync(string key);
		Task<SPayResponse<bool>> CreateCardTypeAsync(CreateOrUpdateCardTypeRequest request);
		Task<SPayResponse<bool>> UpdateCardTypeAsync(string key, CreateOrUpdateCardTypeRequest request);
	}

	public class CardTypeService : ICardTypeService
	{
		private readonly ICardTypeRepository _repo;
		private readonly ICardRepository _repoCard;
		private readonly IMapper _mapper;

		public CardTypeService(ICardTypeRepository repo, ICardRepository repoCard, IMapper mapper)
		{
			_repo = repo;
			_repoCard = repoCard;
			_mapper = mapper;
		}

		public async Task<SPayResponse<bool>> CreateCardTypeAsync(CreateOrUpdateCardTypeRequest request)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				if (request == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Request model is required!");
					return response;
				}
				var createCardTypeInfo = _mapper.Map<CardType>(request);
				if (createCardTypeInfo == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				createCardTypeInfo.CardTypeKey = string.Format("{0}{1}", PrefixKeyConstant.CARD_TYPE, Guid.NewGuid().ToString().ToUpper());
				createCardTypeInfo.InsDate = DateTimeHelper.GetDateTimeNow();
				createCardTypeInfo.Status = (byte)BasicStatusEnum.Available;
				if (!await _repo.CreateCardTypeAsync(createCardTypeInfo))
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = true;
				response.Success = true;
				response.Message = "Create a card type successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<bool>> DeleteCardTypeAsync(string key)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				var existedCardType = await _repo.GetCardTypeByKeyAsync(key);
				if (existedCardType.CardTypeKey.IsNullOrEmpty())
				{
					SPayResponseHelper.SetErrorResponse(response, "Cannot find card type to delete!");
					response.Error = SPayResponseHelper.NOT_FOUND;
					return response;
				}
				var success = await _repo.DeleteCardTypeAsync(existedCardType);
				if (success == false)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = success;
				response.Success = true;
				response.Message = "Card type delete successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<CardTypeResponse>> GetCardTypeByKeyAsync(string key)
		{
			var response = new SPayResponse<CardTypeResponse>();
			try
			{
				var cardType = await _repo.GetCardTypeByKeyAsync(key);
				if (cardType.CardTypeKey.IsNullOrEmpty())
				{
					SPayResponseHelper.SetErrorResponse(response, $"Not found card type with key: {key}");
					return response;
				}
				var res = _mapper.Map<CardTypeResponse>(cardType);
				response.Data = res;
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

		public async Task<SPayResponse<PaginatedList<CardTypeResponse>>> GetListCardTypeAsync(GetListCardTypeRequest request)
		{
			var response = new SPayResponse<PaginatedList<CardTypeResponse>>();
			try
			{
				var cards = await _repo.GetListCardTypeAsync(request);
				if (cards.Count <= 0)
				{
					SPayResponseHelper.SetErrorResponse(response, "Card type has no row in database.");
					return response;
				}
				var res = _mapper.Map<IList<CardTypeResponse>>(cards);
				var count = 0;
				foreach (var item in res)
				{
					item.No = ++count;
					var cardList = await _repoCard.GetListCardAsync(new GetListCardRequest { CardTypeKey = item.CardTypeKey });
					item.TotalCardUse = cardList.Count; 
				}
				response.Data = await res.ToPaginateAsync(request); ;
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

		public async Task<SPayResponse<bool>> UpdateCardTypeAsync(string key, CreateOrUpdateCardTypeRequest request)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				if (request == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Request model is required!");
					return response;
				}

				var existedCardType = await _repo.GetCardTypeByKeyAsync(key);
				if (existedCardType.CardTypeKey.IsNullOrEmpty())
				{
					SPayResponseHelper.SetErrorResponse(response, "Cannot find card type to update!");
					response.Error = SPayResponseHelper.NOT_FOUND;
					return response;
				}

				var updatedCardType = _mapper.Map<CardType>(request);
				if (updatedCardType == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				if (!await _repo.UpdateCardTypeAsync(key, updatedCardType))
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = true;
				response.Success = true;
				response.Message = $"Update the card type with key: {key} successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}
	}
}
