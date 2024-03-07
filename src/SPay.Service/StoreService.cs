using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs;
using SPay.BO.DTOs.Admin;
using SPay.BO.DTOs.Admin.Card.Response;
using SPay.BO.DTOs.Admin.Store.Request;
using SPay.BO.DTOs.Admin.Store.Response;
using SPay.BO.Extention.Paginate;
using SPay.Repository;

namespace SPay.Service
{
	public interface IStoreService
	{
		Task<SPayResponse<PaginatedList<StoreResponse>>> GetAllStoreInfoAsync(GetAllStoreRequest request);
		Task<SPayResponse<PaginatedList<StoreResponse>>> SearchStoreAsync(AdminSearchRequest request);
		Task<SPayResponse<bool>> DeleteStoreAsync(string key);
		Task<SPayResponse<StoreResponse>> GetStoreByKeyAsync(string key);

	}
	public class StoreService : IStoreService
	{
		private readonly IStoreRepository _storeRepository;
		private readonly IMapper _mapper;
		private readonly IWalletRepository _walletRepo;
		public StoreService(IStoreRepository _storeRepository, IMapper _mapper, IWalletRepository walletRepo)
		{
			this._storeRepository = _storeRepository;
			this._mapper = _mapper;
			_walletRepo = walletRepo;
		}
		public async Task<SPayResponse<PaginatedList<StoreResponse>>> GetAllStoreInfoAsync(GetAllStoreRequest request)
		{
			SPayResponse<PaginatedList<StoreResponse>> response = new SPayResponse<PaginatedList<StoreResponse>>();
			try
			{

				var stores = await _storeRepository.GetAllStoreInfo();
				if (stores == null)
				{
					response.Success = false;
					response.Message = "The result of get all store from repository is null";
					return response;
				}
				var storesResponse = _mapper.Map<List<StoreResponse>>(stores);
				var count = 0;
				foreach (var store in storesResponse)
				{
					store.No = ++count;
					store.Balance = await _walletRepo.GetBalanceForStore(store.StoreKey);
				}

				response.Data = await storesResponse.ToPaginateAsync(request);
				response.Success = true;
				response.Message = "Get all store successfully";
				return response;
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			return response;
		}

		public async Task<SPayResponse<PaginatedList<StoreResponse>>> SearchStoreAsync(AdminSearchRequest request)
		{
			SPayResponse<PaginatedList<StoreResponse>> response = new SPayResponse<PaginatedList<StoreResponse>>();
			try
			{
				var keyWord = request.Keyword.Trim();
				if (string.IsNullOrEmpty(keyWord))
				{
					response.Success = false;
					response.Message = "The keyword is null or empty";
					return response;
				}
				var stores = await _storeRepository.SearchByNameAsync(keyWord);
				if (stores.Count <= 0)
				{
					response.Success = false;
					response.Message = "The result of get all store from repository is null";
					return response;
				}
				var storesRes = _mapper.Map<IList<StoreResponse>>(stores);
				var count = 0;
				foreach (var store in storesRes)
				{
					store.No = ++count;
					store.Balance = await _walletRepo.GetBalanceForStore(store.StoreKey);
				}
				response.Data = await storesRes.ToPaginateAsync(request);
				response.Success = true;
				response.Message = $"Search store successfully, have {count} records";
				return response;
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			return response;

		}

		public async Task<SPayResponse<StoreResponse>> GetStoreByKeyAsync(string key)
		{
			SPayResponse<StoreResponse> response = new SPayResponse<StoreResponse>();
			try
			{
				var store = await _storeRepository.GetStoreByIdAsync(key);
				if (store == null)
				{
					response.Success = false;
					response.Message = "Store not found.";
					return response;
				}
				response.Data = _mapper.Map<StoreResponse>(store);
				response.Success = true;
				response.Message = $"Get Store key = {store.StoreKey} successfully";
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			return response;
		}

		public async Task<SPayResponse<bool>> DeleteStoreAsync(string key)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				var storeDelete = await _storeRepository.GetStoreByIdAsync(key);
				if (storeDelete == null)
				{
					response.Success = false;
					response.Message = "Store not found.";
					return response;
				}
				var success = await _storeRepository.DeleteStoreAsync(storeDelete);
				if (success == false)
				{
					response.Success = false;
					response.Message = "Something wrong!";
				}
				response.Data = success;
				response.Success = true;
				response.Message = "Store delete successfully";
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
