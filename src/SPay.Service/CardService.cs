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

namespace SPay.Service
{
	public interface ICardService
	{
		Task<SPayResponse<PaginatedList<CardResponse>>> GetAllCardsAsync(GetAllCardRequest request);
		Task<SPayResponse<CardResponse>> GetCardById(string id);
		Task<SPayResponse<PaginatedList<CardResponse>>> SearchCardAsync(AdminSearchRequest request);
		Task<SPayResponse<bool>> DeleteCardAsync(string key);
	}
	public class CardService : ICardService
	{
		private readonly ICardRepository _cardRepo;
		private readonly IDepositRepository _depRepo;
		private readonly IMapper _mapper;
		public CardService(ICardRepository _cardRepo, IMapper mapper, IDepositRepository depRepo)
		{
			this._cardRepo = _cardRepo;
			this._mapper = mapper;
			this._depRepo = depRepo;
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
					var deposit = await _depRepo.GetDepositByCardIdAsync(item.CardKey);
					string? name = deposit.DepositPackageKeyNavigation.Name;
					string? packageDes = deposit.DepositPackageKeyNavigation.Description;
					item.No = ++count;
					item.Value = deposit.Amount;
					item.PackageName = string.IsNullOrEmpty(name) ? "" : name;
					item.PackageDescription = string.IsNullOrEmpty(packageDes) ? "" : packageDes;
					item.Price = deposit.DepositPackageKeyNavigation.Price;
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
					response.Message = "The result of get all card from repository is null";
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
					var deposit = await _depRepo.GetDepositByCardIdAsync(item.CardKey);
					string? name = deposit.DepositPackageKeyNavigation.Name;
					string? packageDes = deposit.DepositPackageKeyNavigation.Description;
					item.No = ++count;
					item.Value = deposit.Amount;
					item.PackageName = string.IsNullOrEmpty(name) ? "" : name;
					item.PackageDescription = string.IsNullOrEmpty(packageDes) ? "" : packageDes;
					item.Price = deposit.DepositPackageKeyNavigation.Price;
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
