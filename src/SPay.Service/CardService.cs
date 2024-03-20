using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs;
using SPay.BO.DTOs.Admin;
using SPay.BO.DTOs.Card.Request;
using SPay.BO.DTOs.Card.Response;
using SPay.BO.Extention.Paginate;
using SPay.Repository;
using SPay.Repository.Enum;
using SPay.Service.Response;
using SPay.Service.Utils;

namespace SPay.Service
{
    public interface ICardService
	{
		Task<SPayResponse<PaginatedList<CardResponse>>> GetListCardAsync(GetListCardRequest request);
		Task<SPayResponse<CardResponse>> GetCardByKeyAsync(string key);
		Task<SPayResponse<bool>> DeleteCardAsync(string key);
		Task<SPayResponse<bool>> CreateCardAsync(CreateOrUpdateCardRequest request);
		Task<SPayResponse<bool>> UpdateCardAsync(string key, CreateOrUpdateCardRequest request);
	}
	public class CardService : ICardService
	{
		private readonly ICardRepository _repo;
		private readonly IMapper _mapper;

		public CardService(ICardRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<SPayResponse<bool>> CreateCardAsync(CreateOrUpdateCardRequest request)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				if (request == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Request model is required!");
					return response;
				}
				var createCardInfo = _mapper.Map<Card>(request);
				if (createCardInfo == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				createCardInfo.CardNo = DateTimeHelper.GenerateUniqueQRCode();
				createCardInfo.CardKey = string.Format("{0}{1}", PrefixKeyConstant.CARD, Guid.NewGuid().ToString().ToUpper());
				createCardInfo.InsDate = DateTimeHelper.GetDateTimeNow();
				createCardInfo.Status = (byte)BasicStatusEnum.Available;
				if (!await _repo.CreateCardAsync(createCardInfo))
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
				var existedcard = await _repo.GetCardByKeyAsync(key);
				if (existedcard.CardKey.IsNullOrEmpty())
				{
					SPayResponseHelper.SetErrorResponse(response, "Cannot find card to delete!");
					response.Error = SPayResponseHelper.NOT_FOUND;
					return response;
				}
				var success = await _repo.DeleteCardAsync(existedcard);
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

		public async Task<SPayResponse<CardResponse>> GetCardByKeyAsync(string key)
		{
			var response = new SPayResponse<CardResponse>();
			try
			{
				var card = await _repo.GetCardByKeyAsync(key);
				if (card.CardKey.IsNullOrEmpty())
				{
					SPayResponseHelper.SetErrorResponse(response, $"Not found card with key: {key}");
					return response;
				}
				var res = _mapper.Map<CardResponse>(card);
				response.Data = res;
				response.Success = true;
				response.Message = "Get card successfully";
				return response;
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<PaginatedList<CardResponse>>> GetListCardAsync(GetListCardRequest request)
		{
			var response = new SPayResponse<PaginatedList<CardResponse>>();
			try
			{
				var cards = await _repo.GetListCardAsync(request);
				if (cards.Count <= 0)
				{
					SPayResponseHelper.SetErrorResponse(response, "Card has no row in database.");
					return response;
				}
				var res = _mapper.Map<IList<CardResponse>>(cards);
				var count = 0;
				foreach (var item in res)
				{
					item.No = ++count;
				}
				response.Data = await res.ToPaginateAsync(request); ;
				response.Success = true;
				response.Message = "Get list card successfully";
				return response;
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<bool>> UpdateCardAsync(string key, CreateOrUpdateCardRequest request)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				if (request == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Request model is required!");
					return response;
				}

				var existedCard = await _repo.GetCardByKeyAsync(key);
				if (existedCard.CardKey.IsNullOrEmpty())
				{
					SPayResponseHelper.SetErrorResponse(response, "Cannot find card to update!");
					response.Error = SPayResponseHelper.NOT_FOUND;
					return response;
				}

				var updatedCard = _mapper.Map<Card>(request);
				if (updatedCard == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				if (!await _repo.UpdateCardAsync(key, updatedCard))
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = true;
				response.Success = true;
				response.Message = $"Update the card with key: {key} successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}
	}
}
