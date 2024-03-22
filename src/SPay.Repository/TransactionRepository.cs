using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Transaction.Request;

namespace SPay.Repository
{
    public interface ITransactionRepository
    {
        Task<IList<Transaction>> GetListTransactionInfoAsync(GetListTransactionRequest request);
		Task<bool> CreateTransactionAsync(Transaction request);

	}
	public class TransactionRepository : ITransactionRepository
    {
        private readonly SpayDBContext _context;
        public TransactionRepository(SpayDBContext _context)
        {
            this._context = _context;
        }

		public async Task<bool> CreateTransactionAsync(Transaction request)
		{
			_context.Transactions.Add(request);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<IList<Transaction>> GetListTransactionInfoAsync(GetListTransactionRequest request)
        {
            return await _context.Transactions
                .Include(t => t.OrderKeyNavigation)
                .Include(t => t.WithdrawKeyNavigation)
                .Include(t => t.OrderKeyNavigation.MembershipKeyNavigation)
				.ToListAsync();
        }
    }
}
