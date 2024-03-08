using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Admin.Wallet;

namespace SPay.Repository
{
    public interface IWalletRepository
    {
        Task<Wallet> GetBalanceForUser(GetBalanceModel models);
        Task<bool> CreateWalletAsync(Wallet wallet);
    }
	public class WalletRepository : IWalletRepository
    {
        private readonly SPayDbContext _context;
        public WalletRepository(SPayDbContext _context)
        {
            this._context = _context;
        }
        public async Task<Wallet> GetBalanceForUser(GetBalanceModel models)
        {
            var result = await _context.Wallets
                .Where(w => w.StoreKey.Equals(models.StoreKey) 
                && w.CustomerKey == models.CustomerKey 
                && w.CardKey == models.CardKey).FirstOrDefaultAsync();
            return result;
        }

		public async Task<bool> CreateWalletAsync(Wallet wallet)
		{
			await _context.Wallets.AddAsync(wallet);
            return await _context.SaveChangesAsync() > 0;
		}
	}
}
