using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Admin.Transaction.Request;
using SPay.BO.DTOs.Admin.Transaction.Response;
using SPay.BO.DTOs.Admin;
using SPay.BO.DTOs.Admin.Store.Response;
using SPay.BO.Extention.Paginate;
using SPay.Repository;
using SPay.Service.Response;

namespace SPay.Service
{
    public interface ITransactionService
    {
		Task<SPayResponse<PaginatedList<TransactionResponse>>> GetAllTransactionsAsync(GetAllTransactionRequest request);
		Task<SPayResponse<TransactionResponse>> GetTransactionByKeyAsync(string id);
		Task<SPayResponse<bool>> DeleteTransactionAsync(string key);
		Task<SPayResponse<bool>> CreateTransactionAsync(CreateTransactionRequest request);
	}
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repo;
        private readonly IMapper _mapper;
        public TransactionService(ITransactionRepository _repo, IMapper _mapper)
        {
            this._repo = _repo;
            this._mapper = _mapper;
        }

		public async Task<SPayResponse<bool>> CreateTransactionAsync(CreateTransactionRequest request)
		{
			throw new NotImplementedException();
		}

		public async Task<SPayResponse<bool>> DeleteTransactionAsync(string key)
		{
			throw new NotImplementedException();
		}

		public async Task<SPayResponse<PaginatedList<TransactionResponse>>> GetAllTransactionsAsync(GetAllTransactionRequest request)
		{
			throw new NotImplementedException();
		}

		public async Task<SPayResponse<TransactionResponse>> GetTransactionByKeyAsync(string id)
		{
			throw new NotImplementedException();
		}
	}
}
