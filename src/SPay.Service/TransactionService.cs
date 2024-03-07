using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Admin.Store.Response;
using SPay.BO.DTOs.Admin.Transaction.Response;
using SPay.Repository;
using SPay.Service.Response;

namespace SPay.Service
{
    public interface ITransactionService
    {
        Task<SPayResponse<IList<TransactionResponse>>> GetAllTransInfoAsync();
    }
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IMapper _mapper;
        public TransactionService(ITransactionRepository _transactionRepo, IMapper _mapper)
        {
            this._transactionRepo = _transactionRepo;
            this._mapper = _mapper;
        }
        public async Task<SPayResponse<IList<TransactionResponse>>> GetAllTransInfoAsync()
        {
            var result = new SPayResponse<IList<TransactionResponse>>();
            try
            {
                var transList = await _transactionRepo.GetAllTransactionInfoAsync();
                var transRes = _mapper.Map<IList<TransactionResponse>>(transList);
                result.Success = true;
                result.Data = transRes;
                result.Message = "Transaction retrieved successfully";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error in : TransactionService funciton GetAllTransInfoAsync()";
                result.ErrorMessages = new List<string> { ex.Message };
            }
            return result;
        }
    }
}
