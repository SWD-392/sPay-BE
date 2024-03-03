using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;

namespace SPay.Repository
{
    public interface ITransactionRepository
    {
        Task<IList<Transaction>> GetAllTransactionInfoAsync();
    }
    public class TransactionRepository : ITransactionRepository
    {
        private readonly SPayDbContext _context;
        public TransactionRepository(SPayDbContext _context)
        {
            this._context = _context;
        }
        public async Task<IList<Transaction>> GetAllTransactionInfoAsync()
        {
            return await _context.Transactions.ToListAsync();
        }
    }
}
