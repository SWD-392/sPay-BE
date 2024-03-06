using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
//using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
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
        Task<PaginatedList<CardResponse>> GetAllCardsAsync(GetAllCardRequest request);
        Task<PaginatedList<CardResponse>> SearchCardAsync(AdminSearchRequest request);
		public Task<bool> ChangeCardStatus(int id);
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

		public Task<bool> ChangeCardStatus(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<PaginatedList<CardResponse>> GetAllCardsAsync(GetAllCardRequest request)
        {
                var cards = await _cardRepo.GetAllAsync();
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
			PaginatedList<CardResponse> result = await cardsRes.ToPaginateAsync(request);
            return result;
		}

        public async Task<PaginatedList<CardResponse>> SearchCardAsync(AdminSearchRequest request)
        {
                var keyWord = request.Keyword.Trim();
                if (string.IsNullOrEmpty(keyWord))
                {
                    throw new ArgumentNullException(nameof(keyWord));
                }
                var cards = await _cardRepo.SearchCardByNameAsync(keyWord);
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
			PaginatedList<CardResponse> result = await cardsRes.ToPaginateAsync(request);
			return result;
		}
    }
}
