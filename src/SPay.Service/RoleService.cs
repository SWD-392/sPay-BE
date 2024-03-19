using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DTOs;
using SPay.BO.DTOs.Auth.Request;
using SPay.BO.DTOs.PromotionPackage.Request;
using SPay.BO.DTOs.PromotionPackage.Response;
using SPay.BO.DTOs.Role.Response;
using SPay.BO.Extention.Paginate;
using SPay.Repository;
using SPay.Service.Response;

namespace SPay.Service
{
	public interface IRoleService
	{
		Task<SPayResponse<PaginatedList<RoleResponse>>> GetListRoleAsync(GetListRoleRequest request);
	}

	public class RoleService : IRoleService
	{
		private readonly IMapper _mapper;
		private readonly IRoleRepository _repo;

		public RoleService(IMapper mapper, IRoleRepository repo)
		{
			_mapper = mapper;
			_repo = repo;
		}

		public async Task<SPayResponse<PaginatedList<RoleResponse>>> GetListRoleAsync(GetListRoleRequest request)
		{
				var response = new SPayResponse<PaginatedList<RoleResponse>>();
				try
				{
					var promotionPackages = await _repo.GetListRoleAsync();
					if (promotionPackages == null)
					{
						SPayResponseHelper.SetErrorResponse(response, "Roles has no row in database.");
						return response;
					}
					var res = _mapper.Map<IList<RoleResponse>>(promotionPackages);
					var count = 0;
					foreach (var item in res)
					{
						item.No = ++count;
					}
					response.Data = await res.ToPaginateAsync(request); ;
					response.Success = true;
					response.Message = "Get list role successfully";
					return response;

				}
				catch (Exception ex)
				{
					SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
				}
				return response;
		}
	}
}
