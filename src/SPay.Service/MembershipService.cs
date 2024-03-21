using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Membership.Request;
using SPay.BO.DTOs.Membership.Response;
using SPay.BO.Extention.Paginate;
using SPay.Repository.Enum;
using SPay.Repository;
using SPay.Service.Response;
using SPay.Service.Utils;
using Microsoft.IdentityModel.Tokens;

namespace SPay.Service
{
	public interface IMembershipService
	{
		Task<SPayResponse<PaginatedList<MembershipResponse>>> GetListMembershipAsync(GetListMembershipRequest request);
		Task<SPayResponse<MembershipResponse>> GetMembershipByKeyAsync(string key);
		Task<SPayResponse<bool>> CreateMembershipAsync(CreateOrUpdateMembershipRequest request);
		Task<bool> CreateDefaultMembershipAsync(string UserKey);

	}
	public class MembershipService : IMembershipService
	{
		private readonly IMembershipRepository _repo;
		private readonly IMapper _mapper;

		public MembershipService(IMembershipRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<SPayResponse<bool>> CreateMembershipAsync(CreateOrUpdateMembershipRequest request)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				if (request == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Request model is required!");
					return response;
				}
				var createMembershipInfo = _mapper.Map<Membership>(request);
				if (createMembershipInfo == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				createMembershipInfo.MembershipKey = string.Format("{0}{1}", PrefixKeyConstant.MEMBERSHIP, Guid.NewGuid().ToString().ToUpper());
				if (!await _repo.CreateMembershipAsync(createMembershipInfo))
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = true;
				response.Success = true;
				response.Message = "Create a Membership successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<MembershipResponse>> GetMembershipByKeyAsync(string key)
		{
			var response = new SPayResponse<MembershipResponse>();
			try
			{
				var membershipRes = await _repo.GetMembershipByKeyAsync(key);
				if (membershipRes.Membership.MembershipKey.IsNullOrEmpty())
				{
					SPayResponseHelper.SetErrorResponse(response, $"Not found Membership with key: {key}");
					return response;
				}
				var res = _mapper.Map<MembershipResponse>(membershipRes);
				response.Data = res;
				response.Success = true;
				response.Message = "Get Membership successfully";
				return response;
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<PaginatedList<MembershipResponse>>> GetListMembershipAsync(GetListMembershipRequest request)
		{
			var response = new SPayResponse<PaginatedList<MembershipResponse>>();
			try
			{
				var memberships = await _repo.GetListMembershipAsync(request);
				if (memberships.Count <= 0)
				{
					SPayResponseHelper.SetErrorResponse(response, "Membership has no row in database.");
					return response;
				}
				var res = _mapper.Map<IList<MembershipResponse>>(memberships);
				var count = 0;
				foreach (var item in res)
				{
					item.No = ++count;
				}
				response.Data = await res.ToPaginateAsync(request); ;
				response.Success = true;
				response.Message = "Get list Membership successfully";
				return response;
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public Task<bool> CreateDefaultMembershipAsync(string UserKey)
		{
			throw new NotImplementedException();
		}
	}
}
