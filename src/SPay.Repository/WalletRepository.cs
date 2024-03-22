using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;

namespace SPay.Repository
{
    public interface IWalletRepository
    {
        Task<bool> CreateWalletAsync(Wallet wallet);
	}
	public class WalletRepository : IWalletRepository
    {
        private readonly SpayDBContext _context;
        public WalletRepository(SpayDBContext _context)
        {
            this._context = _context;
        }

		public async Task<bool> CreateWalletAsync(Wallet wallet)
		{
		    await _context.AddAsync(wallet);
            return await _context.SaveChangesAsync() > 0;
		}
	}
}
