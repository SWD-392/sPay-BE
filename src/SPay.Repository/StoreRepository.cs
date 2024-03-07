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
        Task<IList<StoreOwner>> GetAllStoreInfo();
        Task<IList<StoreOwner>> SearchByNameAsync(string name);
		Task<StoreOwner> GetStoreByIdAsync(string key);
		Task<bool> DeleteStoreAsync(StoreOwner store);

	}
    public class StoreRepository : IStoreRepository
    {
		private readonly byte DELETED = (byte)CardStatusEnum.Deleted;
		private readonly SPayDbContext _context;
        public StoreRepository(SPayDbContext _context)
        {
            this._context = _context;
        }
        public async Task<IList<StoreOwner>> GetAllStoreInfo()
        {
            return await _context.StoreOwners
                .Include(so => so.StoreKeyNavigation)
                .Include(so => so.StoreKeyNavigation.CategoryKeyNavigation)
                .Include(so => so.StoreKeyNavigation.Wallets)
                .ToListAsync();
        }

        public async Task<IList<StoreOwner>> SearchByNameAsync(string name)
        {
            return await _context.StoreOwners
                .Where(so => so.StoreKeyNavigation.Name.ToLower().Contains(name.ToLower()) && so.StoreKeyNavigation.Status != DELETED)
                .Include(so => so.StoreKeyNavigation)
                .Include(so => so.StoreKeyNavigation.CategoryKeyNavigation)
                .Include(so => so.StoreKeyNavigation.Wallets)
                .ToListAsync();
        }
		public async Task<StoreOwner> GetStoreByIdAsync(string key)
        {
            Test();
			return await _context.StoreOwners
				.Include(so => so.StoreKeyNavigation)
				.Include(so => so.StoreKeyNavigation.CategoryKeyNavigation)
				.Include(so => so.StoreKeyNavigation.Wallets)
				.FirstOrDefaultAsync(c => c.StoreKey == key && c.StoreKeyNavigation.Status != DELETED); ;
		}
		public async Task<bool> DeleteStoreAsync(StoreOwner store)
        {
            store.StoreKeyNavigation.Status = DELETED;
            await _context.SaveChangesAsync();
            return false;
        }

        private void Test()
        {
			var StoreKey = string.Format("{0}{1}", PrefixKeyConstant.STORE, Guid.NewGuid().ToString().ToUpper());
            Console.WriteLine(StoreKey);
		}
	}
}
