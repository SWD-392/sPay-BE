using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SPay.BO.DTOs.Admin;
using SPay.BO.DTOs.Admin.Card.Response;
using SPay.Repository;

namespace SPay.Service
{
    public interface ICardService
    {
        Task<SPayResponse<IList<CardResponse>>> GetAllCardsAsync();
        Task<SPayResponse<IList<CardResponse>>> SearchCardAsync(AdminSearchRequest request);

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
        public async Task<SPayResponse<IList<CardResponse>>> GetAllCardsAsync()
        {
            var result = new SPayResponse<IList<CardResponse>>();
            try
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
                    item.Amount = deposit.Amount;
                    item.PackageName = name.IsNullOrEmpty() ? "" : name;
                    item.PackageDescription = packageDes.IsNullOrEmpty() ? "" : packageDes;
                    item.Price = deposit.DepositPackageKeyNavigation.Price;
                }
                result.Data = cardsRes;
                result.Success = true;
                result.Message = "Card retrieved successfully";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error in : CardService funciton GetAllCardsAsync()";
                result.ErrorMessages = new List<string> { ex.Message };
            }
            return result;
        }

        public async Task<SPayResponse<IList<CardResponse>>> SearchCardAsync(AdminSearchRequest request)
        {
            var result = new SPayResponse<IList<CardResponse>>();
            try
            {
                var keyWord = request.keyword.Trim();
                if (string.IsNullOrEmpty(keyWord))
                {
                    result.Success = false;
                    result.Message = "Keyword null or empty!";
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
                    item.Amount = deposit.Amount;
                    item.PackageName = name.IsNullOrEmpty() ? "" : name;
                    item.PackageDescription = packageDes.IsNullOrEmpty() ? "" : packageDes;
                    item.Price = deposit.DepositPackageKeyNavigation.Price;
                }
                result.Data = cardsRes;
                result.Success = true;
                result.Message = "Search card by cart type successfully";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error in : CardService funciton GetAllCardsAsync()";
                result.ErrorMessages = new List<string> { ex.Message };
            }
            return result;
        }
    }
}
