using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.WithdrawInfomation.Request;
using SPay.BO.DTOs.WithdrawInfomation.Response;
using SPay.BO.Extention.Paginate;
using SPay.Repository;
using SPay.Repository.Enum;
using SPay.Service.Response;
using SPay.Service.Utils;

namespace SPay.Service
{
	public interface IWithdrawInfomationsService
	{
		Task<SPayResponse<PaginatedList<WithdrawInformationResponse>>> GetListWithdrawInfosAsync(GetListWithdrawInfomationRequest request);
		Task<SPayResponse<WithdrawInformationResponse>> GetWithdrawInfoByKeyAsync(string id);
		Task<SPayResponse<bool>> CreateWithdrawInfoAsync(CreateWithdrawInfomationRequest request);
		Task<SPayResponse<bool>> DeleteWithdrawInfoAsync(string key);
	}

	public class WithdrawInfomationsService : IWithdrawInfomationsService
	{
		private readonly IWithdrawInfoRepository _repo;
		private readonly IStoreRepository _repoStore;
		private readonly IUserRepository _repoUser;
		private readonly ITransactionRepository _repoTrans;
		private readonly IMapper _mapper;

		public WithdrawInfomationsService(IWithdrawInfoRepository repo, IStoreRepository repoStore, IUserRepository repoUser, ITransactionRepository repoTrans, IMapper mapper)
		{
			_repo = repo;
			_repoStore = repoStore;
			_repoUser = repoUser;
			_repoTrans = repoTrans;
			_mapper = mapper;
		}

		public async Task<SPayResponse<bool>> CreateWithdrawInfoAsync(CreateWithdrawInfomationRequest request)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
				{
					if (request == null)
					{
						SPayResponseHelper.SetErrorResponse(response, "Request model is required!");
						return response;
					}
					var createWithdraw = _mapper.Map<WithdrawInformation>(request);
					if (createWithdraw == null)
					{
						SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
						return response;
					}
					var user = await _repoUser.GetUserByKeyAsync(createWithdraw.UserKey, isStore: true); // Role customer => limit rút tiền
					if (user == null)
					{
						SPayResponseHelper.SetErrorResponse(response, "Not find store to withdrawl");
						return response;
					}

					createWithdraw.WithdrawKey = string.Format("{0}{1}", PrefixKeyConstant.WITHDRAW_INFO, Guid.NewGuid().ToString().ToUpper());
					createWithdraw.InsDate = DateTimeHelper.GetDateTimeNow();
					createWithdraw.Status = (byte)WithdrawStatusEnum.Succeeded;

					if (!await _repo.CreateWithdrawInfoAsync(createWithdraw))
					{
						SPayResponseHelper.SetErrorResponse(response, "Error when create withdraw information!");
						return response;
					}

					var isProcess = await _repo.ProcessMoney(request);
					if (!isProcess)
					{
						SPayResponseHelper.SetErrorResponse(response, "Error when process money!");
						return response;
					}

					var transactionWithdraw = new SPay.BO.DataBase.Models.Transaction();
					transactionWithdraw.TransactionKey = string.Format("{0}{1}", PrefixKeyConstant.TRANSACTION, Guid.NewGuid().ToString().ToUpper());
					transactionWithdraw.Status = createWithdraw.Status;
					transactionWithdraw.WithdrawKey = createWithdraw.WithdrawKey;
					transactionWithdraw.InsDate = createWithdraw.InsDate;
					if (!await _repoTrans.CreateTransactionAsync(transactionWithdraw))
					{
						throw new Exception("Create transaction failed!");
					};

					response.Data = true;
					response.Success = true;
					response.Message = "Create a withdraw info successfully";

					scope.Complete(); // Hoàn thành giao dịch
				}
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<bool>> DeleteWithdrawInfoAsync(string key)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				try
				{
					var existedWithdraw = await _repo.GetWithdrawInfoByKeyAsync(key);
					if (existedWithdraw == null)
					{
						SPayResponseHelper.SetErrorResponse(response, "Cannot find withdraw infomation to delete!");
						return response;
					}
					var success = await _repo.DeleteWithdrawInfoAsync(existedWithdraw);
					if (success == false)
					{
						SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
						return response;
					}

					scope.Complete();

					response.Data = success;
					response.Success = true;
					response.Message = "Withdraw infomation delete successfully";
				}
				catch (Exception ex)
				{
					// Rollback transaction in case of any error
					SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
				}
			}
			return response;
		}

		public async Task<SPayResponse<PaginatedList<WithdrawInformationResponse>>> GetListWithdrawInfosAsync(GetListWithdrawInfomationRequest request)
		{
			var response = new SPayResponse<PaginatedList<WithdrawInformationResponse>>();
			try
			{
				var withdrawInfos = await _repo.GetAllWithdrawInfoTypeAsync(request);
				if (withdrawInfos == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Withdraw information has no row in database.");
					return response;
				}
				var withdrawInfosRes = _mapper.Map<IList<WithdrawInformationResponse>>(withdrawInfos);
				var count = 0;
				foreach (var item in withdrawInfosRes)
				{
					item.No = ++count;
					if (item.Type.Equals(Constant.WithdrawInfo.CUSTOMER_TYPE))
					{
						item.Description = string.Format(Constant.WithdrawInfo.CUSTOMER_DESCRIPTION, item.UserName, item.TotalAmount);
					}
					if (item.Type.Equals(Constant.WithdrawInfo.STORE_TYPE))
					{
						var stores = await _repoStore.GetListStoreAsync(new BO.DTOs.Store.Request.GetListStoreRequest() { UserKey = item.UserKey });
						var store = stores.Single();
						item.Description = string.Format(Constant.WithdrawInfo.STORE_DESCRIPTION, item.UserName, item.TotalAmount, store.StoreName);
					}
				}
				response.Data = await withdrawInfosRes.ToPaginateAsync(request); ;
				response.Success = true;
				response.Message = "Get withdraw information successfully";
				return response;

			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<WithdrawInformationResponse>> GetWithdrawInfoByKeyAsync(string key)
		{
			var response = new SPayResponse<WithdrawInformationResponse>();
			try
			{
				var withdrawInfos = await _repo.GetWithdrawInfoByKeyAsync(key);
				if (withdrawInfos == null)
				{
					SPayResponseHelper.SetErrorResponse(response, $"Withdraw information key {key} not found.");
					return response;
				}
				var withdrawInfosRes = _mapper.Map<WithdrawInformationResponse>(withdrawInfos);
				if (withdrawInfosRes.Type.Equals(Constant.WithdrawInfo.CUSTOMER_TYPE))
				{
					withdrawInfosRes.Description = string.Format(Constant.WithdrawInfo.CUSTOMER_DESCRIPTION, withdrawInfosRes.UserName, withdrawInfosRes.TotalAmount);
				}
				if (withdrawInfosRes.Type.Equals(Constant.WithdrawInfo.STORE_TYPE))
				{
					var stores = await _repoStore.GetListStoreAsync(new BO.DTOs.Store.Request.GetListStoreRequest() { UserKey = withdrawInfosRes.UserKey });
					var store = stores.Single();
					withdrawInfosRes.Description = string.Format(Constant.WithdrawInfo.STORE_DESCRIPTION, withdrawInfosRes.UserName, withdrawInfosRes.TotalAmount, store.StoreName);
				}
				response.Data = withdrawInfosRes;
				response.Success = true;
				response.Message = "Get withdraw information successfully";
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
