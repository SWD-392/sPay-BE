using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;

namespace SPay.Repository
{
    public interface IDepositRepository
    {
        Task<Deposit> GetDepositByCardIdAsync(string cardKey);
    }
    public class DepositRepository : IDepositRepository
    {
        private readonly SPayDbContext _context;
        public DepositRepository(SPayDbContext _context)
        {
            this._context = _context;
        }
        public Task<Deposit> GetDepositByCardIdAsync(string cardKey)
        {
            var result = _context.Deposits
                .Include(x => x.DepositPackageKeyNavigation)
                .FirstOrDefaultAsync(d => d.CardKey.Equals(cardKey));
            return result;
        }
    }
}
