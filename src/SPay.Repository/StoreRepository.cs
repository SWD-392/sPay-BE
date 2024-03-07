using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.Repository.Enum;

namespace SPay.Repository
{
    public interface IStoreRepository
    {
        Task<IList<Store>> GetAllStoreInfo();
        Task<IList<Store>> SearchByNameAsync(string name);
		Task<Store> GetStoreByIdAsync(string key);
		Task<bool> DeleteStoreAsync(Store store);

	}
    public class StoreRepository : IStoreRepository
    {
		private readonly SPayDbContext _context;
        public StoreRepository(SPayDbContext _context)
        {
            this._context = _context;
        }
        public async Task<IList<Store>> GetAllStoreInfo()
        {
            return await _context.Stores
                .Include(so => so.CategoryKeyNavigation)
                .Include(so => so.Wallets)
                .ToListAsync();
        }

        public async Task<IList<Store>> SearchByNameAsync(string name)
        {
            return await _context.Stores
                .Where(s => s.Name.ToLower().Contains(name.ToLower()) && !isDeleted(s.Status))
                .Include(s => s.CategoryKeyNavigation)
                .Include(s => s.Wallets)
                .ToListAsync();
        }
		public async Task<Store> GetStoreByIdAsync(string key)
        {
			return await _context.Stores
				.Include(s => s.CategoryKeyNavigation)
				.Include(s => s.Wallets)
				.FirstOrDefaultAsync(s => s.StoreKey == key && !isDeleted(s.Status)); ;
		}
		public async Task<bool> DeleteStoreAsync(Store store)
        {
            store.Status = (byte)CardStatusEnum.Deleted;
            await _context.SaveChangesAsync();
            return false;
        }

        private bool isDeleted(byte status)
        {
            return status == (byte)CardStatusEnum.Deleted;
        }

        private void Test()
        {
			var StoreKey = string.Format("{0}{1}", PrefixKeyConstant.STORE, Guid.NewGuid().ToString().ToUpper());
            Console.WriteLine(StoreKey);
		}
	}
}
