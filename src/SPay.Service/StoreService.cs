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
using SPay.BO.DTOs.Admin.Wallet;
using SPay.BO.DTOs.Store.Request;
using SPay.BO.DTOs.Store.Response;
using SPay.BO.Extention.Paginate;
using SPay.Repository;
using SPay.Repository.Enum;
using SPay.Service.Response;
using SPay.Service.Utils;
using Microsoft.IdentityModel.Tokens;

namespace SPay.Service
{
    public interface IStoreService
	{
		Task<SPayResponse<PaginatedList<StoreResponse>>> GetListStoreAsync(GetListStoreRequest request);
		Task<SPayResponse<StoreResponse>> GetStoreByKeyAsync(string key);
		Task<SPayResponse<bool>> DeleteStoreAsync(string key);
		Task<SPayResponse<bool>> CreateStoreAsync(CreateOrUpdateStoreRequest request);
		Task<SPayResponse<bool>> UpdateStoreAsync(string key, CreateOrUpdateStoreRequest request);

	}
	public class StoreService : IStoreService
	{
		private readonly IStoreRepository _repo;
		private readonly IMapper _mapper;

		public StoreService(IStoreRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<SPayResponse<bool>> CreateStoreAsync(CreateOrUpdateStoreRequest request)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				if (request == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Request model is required!");
					return response;
				}
				var createStoreInfo = _mapper.Map<Store>(request);
				if (createStoreInfo == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				createStoreInfo.StoreKey = string.Format("{0}{1}", PrefixKeyConstant.STORE_CATE, Guid.NewGuid().ToString().ToUpper());
				createStoreInfo.InsDate = DateTimeHelper.GetDateTimeNow();
				createStoreInfo.Status = (byte)BasicStatusEnum.Available;
				if (!await _repo.CreateStoreAsync(createStoreInfo))
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = true;
				response.Success = true;
				response.Message = "Create a store successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<bool>> DeleteStoreAsync(string key)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				var existedStore = await _repo.GetStoreByKeyAsync(key);

				var success = await _repo.DeleteStoreAsync(existedStore);
				if (success == false)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = success;
				response.Success = true;
				response.Message = "Store delete successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<PaginatedList<StoreResponse>>> GetListStoreAsync(GetListStoreRequest request)
		{
			var response = new SPayResponse<PaginatedList<StoreResponse>>();
			try
			{
				var storeCates = await _repo.GetListStoreAsync(request);
				if (storeCates.Count <= 0)
				{
					SPayResponseHelper.SetErrorResponse(response, "Store has no row in database.");
					return response;
				}
				var res = _mapper.Map<IList<StoreResponse>>(storeCates);
				var count = 0;
				foreach (var item in res)
				{
					item.No = ++count;
				}
				response.Data = await res.ToPaginateAsync(request); ;
				response.Success = true;
				response.Message = "Get list store successfully";
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
				var storeCate = await _repo.GetStoreByKeyAsync(key);
				var res = _mapper.Map<StoreResponse>(storeCate);
				response.Data = res;
				response.Success = true;
				response.Message = "Get store successfully";
				return response;
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<bool>> UpdateStoreAsync(string key, CreateOrUpdateStoreRequest request)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				if (request == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Request model is required!");
					return response;
				}

				var existedStore = await _repo.GetStoreByKeyAsync(key);

				var updatedStore = _mapper.Map<Store>(request);
				if (updatedStore == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				await _repo.UpdateStoreAsync(key, updatedStore);

				response.Data = true;
				response.Success = true;
				response.Message = $"Update the store with key: {key} successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}
	}
}
