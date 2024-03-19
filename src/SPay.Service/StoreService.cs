//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Transactions;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using SPay.BO.DataBase.Models;
//using SPay.BO.DTOs;
//using SPay.BO.DTOs.Admin;
//using SPay.BO.DTOs.Admin.Card.Response;
//using SPay.BO.DTOs.Admin.Store.Request;
//using SPay.BO.DTOs.Admin.Store.Response;
//using SPay.BO.DTOs.Admin.User;
//using SPay.BO.DTOs.Admin.Wallet;
//using SPay.BO.Extention.Paginate;
//using SPay.Repository;
//using SPay.Repository.Enum;
//using SPay.Service.Response;

//namespace SPay.Service
//{
//    public interface IStoreService
//	{
//		Task<SPayResponse<PaginatedList<StoreResponse>>> GetAllStoreInfoAsync(GetAllStoreRequest request);
//		Task<SPayResponse<PaginatedList<StoreResponse>>> SearchStoreAsync();
//		Task<SPayResponse<bool>> DeleteStoreAsync(string key);
//		Task<SPayResponse<bool>> CreateStoreAsync(CreateStoreRequest request);
//		Task<SPayResponse<StoreResponse>> GetStoreByKeyAsync(string key);
//		Task<SPayResponse<IList<StoreCateResponse>>> GetAllStoreCateAsync();
//		Task<SPayResponse<StoreCateResponse>> GetStoreCateByKeyAsync(string storeCateKey);

//	}
//	public class StoreService : IStoreService
//	{
//		private readonly IStoreRepository _storeRepository;
//		private readonly IMapper _mapper;
//		private readonly IWalletRepository _walletRepo;
//		private readonly IWalletService _walletService;
//		private readonly IUserService _userService;

//		public StoreService(IStoreRepository _storeRepository, IMapper _mapper, IWalletRepository walletRepo, IWalletService _walletService, IUserService _userService)
//		{
//			this._storeRepository = _storeRepository;
//			this._mapper = _mapper;
//			_walletRepo = walletRepo;
//			this._walletService = _walletService;
//			this._userService = _userService;
//		}

//		public Task<SPayResponse<bool>> CreateStoreAsync(CreateStoreRequest request)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<SPayResponse<bool>> DeleteStoreAsync(string key)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<SPayResponse<IList<StoreCateResponse>>> GetAllStoreCateAsync()
//		{
//			throw new NotImplementedException();
//		}

//		public Task<SPayResponse<PaginatedList<StoreResponse>>> GetAllStoreInfoAsync(GetAllStoreRequest request)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<SPayResponse<StoreResponse>> GetStoreByKeyAsync(string key)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<SPayResponse<StoreCateResponse>> GetStoreCateByKeyAsync(string storeCateKey)
//		{
//			throw new NotImplementedException();
//		}

//		public Task<SPayResponse<PaginatedList<StoreResponse>>> SearchStoreAsync()
//		{
//			throw new NotImplementedException();
//		}
//	}
//}
