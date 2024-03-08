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
        Task<Wallet> GetBalanceOfUserAsync(GetBalanceModel models);
        Task<bool> CreateWalletAsync(Wallet wallet);
		Task<IList<Wallet>> GetWalletCardByUserKeyAsync(string userKey);
	}
	public class WalletRepository : IWalletRepository
    {
        private readonly SPayDbContext _context;
        public WalletRepository(SPayDbContext _context)
        {
            this._context = _context;
        }
        public async Task<Wallet> GetBalanceOfUserAsync(GetBalanceModel models)
        {
            var result = await _context.Wallets
                .FirstOrDefaultAsync(w => w.StoreKey.Equals(models.StoreKey) 
                && w.CustomerKey.Equals(models.CustomerKey) 
                && w.CardKey.Equals(models.CardKey));
            return result;
        }

		public async Task<bool> CreateWalletAsync(Wallet wallet)
		{
			await _context.Wallets.AddAsync(wallet);
            return await _context.SaveChangesAsync() > 0;
		}

		public async Task<IList<Wallet>> GetWalletCardByUserKeyAsync(string userKey)
		{
			var result = await _context.Wallets
				.Where(w => w.CustomerKey.Equals(userKey)
				&& !string.IsNullOrEmpty(w.CardKey)).ToListAsync();
			return result;
		}
	}
}
