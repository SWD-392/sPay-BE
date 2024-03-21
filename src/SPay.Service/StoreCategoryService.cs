using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPay.BO.Extention.Paginate;
using SPay.Service.Response;
using SPay.Repository;
using AutoMapper;
using SPay.BO.DTOs.CardType.Response;
using Microsoft.IdentityModel.Tokens;
using SPay.BO.DataBase.Models;
using SPay.Repository.Enum;
using SPay.Service.Utils;
using SPay.BO.DTOs.StoreCategory.Response;
using SPay.BO.DTOs.StoreCategory.Request;

namespace SPay.Service
{
	public interface IStoreCategoryService
	{
		Task<SPayResponse<PaginatedList<StoreCateResponse>>> GetListStoreCateAsync(GetListStoreCateRequest request);
		Task<SPayResponse<StoreCateResponse>> GetStoreCateByKeyAsync(string key);
		Task<SPayResponse<bool>> DeleteStoreCateAsync(string key);
		Task<SPayResponse<bool>> CreateStoreCateAsync(CreateOrUpdateStoreCateRequest request);
		Task<SPayResponse<bool>> UpdateStoreCateAsync(string key, CreateOrUpdateStoreCateRequest request);
	}

	public class StoreCategoryService : IStoreCategoryService
	{
		private readonly IStoreCategoryRepository _repo;
		private readonly IMapper _mapper;

		public StoreCategoryService(IStoreCategoryRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<SPayResponse<bool>> CreateStoreCateAsync(CreateOrUpdateStoreCateRequest request)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				if (request == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Request model is required!");
					return response;
				}
				var createStoreCateInfo = _mapper.Map<StoreCategory>(request);
				if (createStoreCateInfo == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				createStoreCateInfo.StoreCategoryKey = string.Format("{0}{1}", PrefixKeyConstant.STORE_CATE, Guid.NewGuid().ToString().ToUpper());
				createStoreCateInfo.InsDate = DateTimeHelper.GetDateTimeNow();
				createStoreCateInfo.Status = (byte)BasicStatusEnum.Available;
				if (!await _repo.CreateStoreCategoryAsync(createStoreCateInfo))
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = true;
				response.Success = true;
				response.Message = "Create a store category successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<bool>> DeleteStoreCateAsync(string key)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				var existedStoreCate = await _repo.GetStoreCategoryByKeyAsync(key);
				var success = await _repo.DeleteStoreCategoryAsync(existedStoreCate);
				if (success == false)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = success;
				response.Success = true;
				response.Message = "Store cate delete successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<PaginatedList<StoreCateResponse>>> GetListStoreCateAsync(GetListStoreCateRequest request)
		{
			var response = new SPayResponse<PaginatedList<StoreCateResponse>>();
			try
			{
				var storeCates = await _repo.GetListStoreCategoryAsync(request);
				if (storeCates.Count <= 0)
				{
					SPayResponseHelper.SetErrorResponse(response, "Store category has no row in database.");
					return response;
				}
				var res = _mapper.Map<IList<StoreCateResponse>>(storeCates);
				var count = 0;
				foreach (var item in res)
				{
					item.No = ++count;
				}
				response.Data = await res.ToPaginateAsync(request); ;
				response.Success = true;
				response.Message = "Get list store category successfully";
				return response;
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<StoreCateResponse>> GetStoreCateByKeyAsync(string key)
		{
			var response = new SPayResponse<StoreCateResponse>();
			try
			{
				var storeCate = await _repo.GetStoreCategoryByKeyAsync(key);
				var res = _mapper.Map<StoreCateResponse>(storeCate);
				response.Data = res;
				response.Success = true;
				response.Message = "Get store category successfully";
				return response;
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<bool>> UpdateStoreCateAsync(string key, CreateOrUpdateStoreCateRequest request)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				if (request == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Request model is required!");
					return response;
				}

				var existedStoreCate = await _repo.GetStoreCategoryByKeyAsync(key);

				var updatedStoreCate = _mapper.Map<StoreCategory>(request);
				if (updatedStoreCate == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				await _repo.UpdateStoreCategoryAsync(key, updatedStoreCate);
				response.Data = true;
				response.Success = true;
				response.Message = $"Update the store category with key: {key} successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}
	}
}
