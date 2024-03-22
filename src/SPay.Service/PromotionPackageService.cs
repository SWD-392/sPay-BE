using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Card.Request;
using SPay.BO.DTOs.PromotionPackage.Request;
using SPay.BO.DTOs.PromotionPackage.Response;
using SPay.BO.Extention.Paginate;
using SPay.Repository;
using SPay.Repository.Enum;
using SPay.Service.Response;
using SPay.Service.Utils;

namespace SPay.Service
{
	public interface IPromotionPackageService
	{
		Task<SPayResponse<PaginatedList<PromotionPackageResponse>>> GetListPromotionPackageAsync(GetListPromotionPackageResquest request);
		Task<SPayResponse<PromotionPackageResponse>> GetPromotionPackageByKeyAsync(string key);
		Task<SPayResponse<bool>> DeletePromotionPackageAsync(string key);
		Task<SPayResponse<bool>> CreatePromotionPackageAsync(CreateOrUpdatePromotionPackageRequest request);
		Task<SPayResponse<bool>> UpdatePromotionPackageAsync(string key, CreateOrUpdatePromotionPackageRequest request);
	}

	public class PromotionPackageService : IPromotionPackageService
	{
		private readonly IPromotionPackageRepository _repo;
		private readonly ICardRepository _repoCard;
		private readonly IMapper _mapper;

		public PromotionPackageService(IPromotionPackageRepository repo, ICardRepository repoCard, IMapper mapper)
		{
			_repo = repo;
			_repoCard = repoCard;
			_mapper = mapper;
		}

		public async Task<SPayResponse<PaginatedList<PromotionPackageResponse>>> GetListPromotionPackageAsync(GetListPromotionPackageResquest request)
		{
			var response = new SPayResponse<PaginatedList<PromotionPackageResponse>>();
			try
			{
				var promotionPackages = await _repo.GetListPromotionPackageAsync();
				if (promotionPackages.Count <= 0)
				{
					SPayResponseHelper.SetErrorResponse(response, "Promotion package has no row in database.");
					return response;
				}
				var res = _mapper.Map<IList<PromotionPackageResponse>>(promotionPackages);
				var count = 0;
				foreach (var item in res)
				{
					item.No = ++count;
					var cardList = await _repoCard.GetListCardAsync(new GetListCardRequest { PromotionPackageKey = item.PromotionPackageKey });
					item.TotalCardUse = cardList.Count;
				}
				response.Data = await res.ToPaginateAsync(request); ;
				response.Success = true;
				response.Message = "Get promotion package successfully";
				return response;

			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<PromotionPackageResponse>> GetPromotionPackageByKeyAsync(string key)
		{
			var response = new SPayResponse<PromotionPackageResponse>();
			try
			{
				var promotionPackage = await _repo.GetPromotionPackageByKeyAsync(key);
				var res = _mapper.Map<PromotionPackageResponse>(promotionPackage);
				response.Data = res;
				response.Success = true;
				response.Message = "Get promotion package successfully";
				return response;
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<bool>> DeletePromotionPackageAsync(string key)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				var cardList = (await _repoCard.GetListCardAsync(new GetListCardRequest { PromotionPackageKey = key })).Count();

				if (cardList > 0)
				{
					throw new Exception("Cannot update the promotion package arleady using in card");
				}
				var existedProPackage = await _repo.GetPromotionPackageByKeyAsync(key);
				var success = await _repo.DeletePromotionPackageAsync(existedProPackage);
				if (success == false)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = success;
				response.Success = true;
				response.Message = "Promotion package delete successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<bool>> CreatePromotionPackageAsync(CreateOrUpdatePromotionPackageRequest request)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				if (request == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Request model is required!");
					return response;
				}
				var createPackageInfo = _mapper.Map<PromotionPackage>(request);
				if (createPackageInfo == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				createPackageInfo.PromotionPackageKey = string.Format("{0}{1}", PrefixKeyConstant.PROMOTION_PACKAGE, Guid.NewGuid().ToString().ToUpper());
				createPackageInfo.InsDate = DateTimeHelper.GetDateTimeNow();
				createPackageInfo.Status = (byte)BasicStatusEnum.Available;
				if (!await _repo.CreatePromotionPackageAsync(createPackageInfo))
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = true;
				response.Success = true;
				response.Message = "Create a promotion package successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<bool>> UpdatePromotionPackageAsync(string key, CreateOrUpdatePromotionPackageRequest request)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				var cardList = (await _repoCard.GetListCardAsync(new GetListCardRequest { PromotionPackageKey = key })).Count();
				if (cardList > 0)
				{
					throw new Exception("Cannot update the promotion package arleady using in card");
				}

				if (request == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Request model is required!");
					return response;
				}

				var existedProPackage = await _repo.GetPromotionPackageByKeyAsync(key);

				var updatedPackage = _mapper.Map<PromotionPackage>(request);
				if (updatedPackage == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong in Mapper!");
					return response;
				}
				await _repo.UpdatePromotionPackageAsync(key, updatedPackage);

				response.Data = true;
				response.Success = true;
				response.Message = $"Update the promotion package with key: {key} successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}
	}
}
