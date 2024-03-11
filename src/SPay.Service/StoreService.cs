using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs;
using SPay.BO.DTOs.Admin;
using SPay.BO.DTOs.Admin.Card.Response;
using SPay.BO.DTOs.Admin.Store.Request;
using SPay.BO.DTOs.Admin.Store.Response;
using SPay.BO.DTOs.Admin.User;
using SPay.BO.DTOs.Admin.Wallet;
using SPay.BO.Extention.Paginate;
using SPay.Repository;
using SPay.Repository.Enum;
using SPay.Service.Response;

namespace SPay.Service
{
    public interface IStoreService
	{
		Task<SPayResponse<PaginatedList<StoreResponse>>> GetAllStoreInfoAsync(GetAllStoreRequest request);
		Task<SPayResponse<PaginatedList<StoreResponse>>> SearchStoreAsync(AdminSearchRequest request);
		Task<SPayResponse<bool>> DeleteStoreAsync(string key);
		Task<SPayResponse<bool>> CreateStoreAsync(CreateStoreRequest request);
		Task<SPayResponse<StoreResponse>> GetStoreByKeyAsync(string key);
	}
	public class StoreService : IStoreService
	{
		private readonly IStoreRepository _storeRepository;
		private readonly IMapper _mapper;
		private readonly IWalletRepository _walletRepo;
		private readonly IWalletService _walletService;
		private readonly IUserService _userService;

		public StoreService(IStoreRepository _storeRepository, IMapper _mapper, IWalletRepository walletRepo, IWalletService _walletService, IUserService _userService)
		{
			this._storeRepository = _storeRepository;
			this._mapper = _mapper;
			_walletRepo = walletRepo;
			this._walletService = _walletService;
			this._userService = _userService;
		}
		public async Task<SPayResponse<PaginatedList<StoreResponse>>> GetAllStoreInfoAsync(GetAllStoreRequest request)
		{
			var response = new SPayResponse<PaginatedList<StoreResponse>>();
			try
			{

				var stores = await _storeRepository.GetAllStoreInfo();
				if (stores == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "The result of get all store from repository is null");
					return response;
				}
				var storesResponse = _mapper.Map<List<StoreResponse>>(stores);
				var count = 0;
				foreach (var store in storesResponse)
				{
					store.No = ++count;
					store.Balance = await _walletService.GetBalanceOfUserAsync(new GetBalanceModel { StoreKey = store.StoreKey });
				}

				response.Data = await storesResponse.ToPaginateAsync(request);
				response.Success = true;
				response.Message = "Get all store successfully";
				return response;
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<PaginatedList<StoreResponse>>> SearchStoreAsync(AdminSearchRequest request)
		{
			var response = new SPayResponse<PaginatedList<StoreResponse>>();
			try
			{
				var keyWord = request.Keyword.Trim();
				if (string.IsNullOrEmpty(keyWord))
				{
					SPayResponseHelper.SetErrorResponse(response, "The keyword is null or empty");
					return response;
				}
				var stores = await _storeRepository.SearchByNameAsync(keyWord);
				if (stores.Count <= 0)
				{
					SPayResponseHelper.SetErrorResponse(response, "The result of get all store from repository is null");
					return response;
				}
				var storesRes = _mapper.Map<IList<StoreResponse>>(stores);
				var count = 0;
				foreach (var store in storesRes)
				{
					store.No = ++count;
					store.Balance = await _walletService.GetBalanceOfUserAsync(new GetBalanceModel { StoreKey =store.StoreKey });
				}
				response.Data = await storesRes.ToPaginateAsync(request);
				response.Success = true;
				response.Message = $"Search store successfully, have {count} records";
				return response;
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;

		}

		public async Task<SPayResponse<StoreResponse>> GetStoreByKeyAsync(string key)
		{
			var response = new SPayResponse<StoreResponse>();

			try
			{
				var store = await _storeRepository.GetStoreByIdAsync(key);

				if (store == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Store not found.");
					return response;
				}

				response.Data = _mapper.Map<StoreResponse>(store);
				response.Success = true;
				response.Message = $"Get Store key = {store.StoreKey} successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}

			return response;
		}

		public async Task<SPayResponse<bool>> DeleteStoreAsync(string key)
		{
			var response = new SPayResponse<bool>();

			try
			{
				var storeDelete = await _storeRepository.GetStoreByIdAsync(key);

				if (storeDelete == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Store not found.");
					return response;
				}

				var success = await _storeRepository.DeleteStoreAsync(storeDelete);

				if (!success)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something wrong!");
					return response;
				}

				response.Data = true;
				response.Success = true;
				response.Message = "Store delete successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}

			return response;
		}

		public async Task<SPayResponse<bool>> CreateStoreAsync(CreateStoreRequest request)
		{
			using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				var response = new SPayResponse<bool>();

				try
				{
					var userKey = string.Format("{0}{1}", PrefixKeyConstant.USER, Guid.NewGuid().ToString().ToUpper());
					var user = new CreateUserModel
					{
						UserKey = userKey,
						NumberPhone = request.PhoneNumber,
						Password = request.Password,
						Role = (int)RoleEnum.Store,
						FullName = request.OwnerName
					};

					if (!await _userService.CreateUserAsync(user))
					{
						SPayResponseHelper.SetErrorResponse(response, "Cannot create User, so cannot create Store");
						return response;
					}

					var storeKey = string.Format("{0}{1}", PrefixKeyConstant.STORE, Guid.NewGuid().ToString().ToUpper());
					var store = new Store
					{
						StoreKey = storeKey,
						Name = request.StoreName,
						CategoryKey = request.StoreCategoryKey,
						Phone = request.PhoneNumber,
						Status = (byte)StoreStatusEnum.Active,
						Description = request.Description,
						UserKey = userKey,
					};

					if (!await _storeRepository.CreateStoreAsync(store))
					{
						SPayResponseHelper.SetErrorResponse(response, "Cannot create User, so cannot create Store");
						return response;
					}

					var walletKey = string.Format("{0}{1}", PrefixKeyConstant.WALLET, Guid.NewGuid().ToString().ToUpper());
					var storeWallet = new CreateWalletModel
					{
						WalletKey = walletKey,
						WalletTypeKey = WalletTypeKeyConstant.STORE_WALLET,
						StoreKey = storeKey
					};

					if (!await _walletService.CreateWalletAsync(storeWallet))
					{
						SPayResponseHelper.SetErrorResponse(response, "Store create successfully but fail to create wallet");
						return response;
					}

					if (!await _storeRepository.UpdateStoreAfterFirstCreateAsync(storeKey, walletKey))
					{
						SPayResponseHelper.SetErrorResponse(response, "Store and wallet create successfully but fail assign Wallet_Key for store created!");
						return response;
					}

					// Gọi Complete() để commit giao dịch nếu mọi thứ thành công
					transactionScope.Complete();

					response.Data = true;
					response.Success = true;
					response.Message = "Store create successfully";
				}
				catch (Exception ex)
				{
					SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
					// Không gọi Complete(), giao dịch sẽ tự động rollback khi thoát khỏi khối using
				}
				return response; 
			}
		}
	}
}
