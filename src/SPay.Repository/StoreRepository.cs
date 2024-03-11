using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        Task<bool> CreateStoreAsync(Store store);
        Task<bool> UpdateStoreAfterFirstCreateAsync(string storeKey, string walletKey);
        Task<IList<StoreCategory>> GetAllStoreCateAsync();
		Task<StoreCategory> GetStoreCateAsync(string storeCateKey);


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
				.Where(s => s.Status != (byte)CardStatusEnum.Deleted)
				.Include(s => s.CategoryKeyNavigation)
				.Include(s => s.UserKeyNavigation)
				.Include(s => s.Wallets)
                .OrderByDescending(s => s.UserKeyNavigation.InsDate)
                .ToListAsync();
        }

		public async Task<IList<StoreCategory>> GetAllStoreCateAsync()
		{
			return await _context.StoreCategories.ToListAsync();
		}
		public async Task<IList<Store>> SearchByNameAsync(string name)
        {
            return await _context.Stores
                .Where(s => s.Name.ToLower().Contains(name.ToLower()) && s.Status != (byte)CardStatusEnum.Deleted)
                .Include(s => s.CategoryKeyNavigation)
                .Include(s => s.UserKeyNavigation)
                .Include(s => s.Wallets)
				.OrderBy(s => s.Name)
				.ToListAsync();
        }
		public async Task<Store> GetStoreByIdAsync(string key)
		{
			return await _context.Stores
				.Include(s => s.CategoryKeyNavigation)
				.Include(s => s.UserKeyNavigation)
				.Include(s => s.Wallets)
				.SingleOrDefaultAsync(s => s.StoreKey == key && s.Status != (byte)CardStatusEnum.Deleted);
		}

		public async Task<bool> DeleteStoreAsync(Store store)
        {
            store.Status = (byte)CardStatusEnum.Deleted;
            return await _context.SaveChangesAsync() > 0;
		}


        private void Test()
        {
			var StoreKey = string.Format("{0}{1}", PrefixKeyConstant.STORE, Guid.NewGuid().ToString().ToUpper());
            Console.WriteLine(StoreKey);
		}

        public async Task<bool> CreateStoreAsync(Store store)
        {
            _context.Stores.Add(store);
            return await _context.SaveChangesAsync() > 0;
        }

		public async Task<bool> UpdateStoreAfterFirstCreateAsync(string storeKey, string walletKey)
		{
            var store = await _context.Stores.SingleOrDefaultAsync(s => s.StoreKey.Equals(storeKey));
            store.WalletKey = walletKey;
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<StoreCategory> GetStoreCateAsync(string storeCateKey)
		{
			return await _context.StoreCategories.SingleOrDefaultAsync(s => s.StoreCategoryKey.Equals(storeCateKey));
		}
	}
}
