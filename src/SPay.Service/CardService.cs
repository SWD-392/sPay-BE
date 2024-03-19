//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AutoMapper;
////using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.IdentityModel.Tokens;
//using SPay.BO.DataBase.Models;
//using SPay.BO.DTOs;
//using SPay.BO.DTOs.Admin;
//using SPay.BO.DTOs.Admin.Card.Request;
//using SPay.BO.DTOs.Admin.Card.Response;
//using SPay.BO.Extention.Paginate;
//using SPay.Repository;
//using SPay.Repository.Enum;
//using SPay.Service.Response;
//using SPay.Service.Utils;

//namespace SPay.Service
//{
//	public interface ICardService
//	{
//		Task<SPayResponse<PaginatedList<CardResponse>>> GetListCardsAsync(GetListCardRequest request);
//		Task<SPayResponse<CardResponse>> GetCardByKeyAsync(string id);
//		Task<SPayResponse<bool>> DeleteCardAsync(string key);
//		Task<SPayResponse<bool>> CreateCardAsync(CreateCardRequest request);
//		Task<int> CountCardByUserKey(string key);
//		#region CardType
//		Task<SPayResponse<IList<CardTypeResponse>>> GetAllCardTypeAsync();
//		Task<SPayResponse<IList<CardTypeResponse>>> GetCardTypeByStoreCateKeyAsync(string storeCateKey); 
//		#endregion
//	}
//	public class CardService : ICardService
//	{
//		private readonly ICardRepository _cardRepo;
//		private readonly IMapper _mapper;
//		private readonly IWalletService _walletService;
//		public CardService(ICardRepository _cardRepo, IMapper mapper, IWalletService _walletService)
//		{
//			this._cardRepo = _cardRepo;
//			this._mapper = mapper;
//			this._walletService = _walletService;
//		}

//		public Task<int> CountCardByUserKey(string key)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<SPayResponse<bool>> CreateCardAsync(CreateCardRequest request)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<SPayResponse<bool>> DeleteCardAsync(string key)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<SPayResponse<IList<CardTypeResponse>>> GetAllCardTypeAsync()
//		{
//			throw new NotImplementedException();
//		}

//		public Task<SPayResponse<CardResponse>> GetCardByKeyAsync(string id)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<SPayResponse<IList<CardTypeResponse>>> GetCardTypeByStoreCateKeyAsync(string storeCateKey)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<SPayResponse<IList<CardResponse>>> GetListCardKeyByCustomerKey(string customerKey)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<SPayResponse<PaginatedList<CardResponse>>> GetListCardsAsync(GetListCardRequest request)
//		{
//			throw new NotImplementedException();
//		}
//	}
//}
