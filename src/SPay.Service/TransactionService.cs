using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DataBase.Models;
using SPay.BO.Extention.Paginate;
using SPay.Repository;
using SPay.Service.Response;
using SPay.BO.DTOs.Store.Response;
using SPay.Repository.Enum;
using SPay.Service.Utils;
using SPay.BO.DTOs.Transaction.Response;
using SPay.BO.DTOs.Transaction.Request;

namespace SPay.Service
{
    public interface ITransactionService
    {
		Task<SPayResponse<PaginatedList<TransactionResponse>>> GetAllTransactionsAsync(GetListTransactionRequest request);
		//Task<SPayResponse<TransactionResponse>> GetTransactionByKeyAsync(string id);
		//Task<SPayResponse<bool>> DeleteTransactionAsync(string key);
		//Task<SPayResponse<bool>> CreateTransactionAsync(CreateTransactionRequest request);
	}
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repo;
		private readonly IUserRepository _repoU;
		private readonly IStoreRepository _repoS;
		private readonly IMapper _mapper;

		public TransactionService(ITransactionRepository repo, IUserRepository repoU, IStoreRepository repoS, IMapper mapper)
		{
			_repo = repo;
			_repoU = repoU;
			_repoS = repoS;
			_mapper = mapper;
		}
		public async Task<SPayResponse<PaginatedList<TransactionResponse>>> GetAllTransactionsAsync(GetListTransactionRequest request)
		{
			var response = new SPayResponse<PaginatedList<TransactionResponse>>();
			try
			{
				var transactions = await _repo.GetListTransactionInfoAsync(request);
				if (transactions.Count <= 0)
				{
					SPayResponseHelper.SetErrorResponse(response, "Store has no row in database.");
					return response;
				}
				var res = _mapper.Map<IList<TransactionResponse>>(transactions);
				var count = 0;
				foreach (var item in res)
				{
					item.No = ++count;
					if(item.Type.Equals(Constant.Transaction.TYPE_PURCHASE))
					{
						item.Receiver = (await _repoS.GetStoreByKeyAsync(item.Receiver)).StoreName;
						item.Sender = (await _repoU.GetUserByKeyAsync(item.Sender)).Fullname;
						item.DescriptionTrans = string.Format(Constant.Transaction.DES_FOR_PURCHASE, item.Type, item.Sender, item.Sender, item.Amount);
					}
					else
					{
						item.Receiver = (await _repoU.GetUserByKeyAsync(item.Receiver)).Fullname;
						item.DescriptionTrans = string.Format(Constant.Transaction.DES_FOR_WITHDRAWL, item.Type, item.Receiver, item.Amount);

					}
					item.Status = EnumHelper.GetDescription((TransactionStatusEnum)Enum.Parse(typeof(TransactionStatusEnum), item.Status));
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
	}
}
