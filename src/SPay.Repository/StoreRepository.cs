using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Store.Request;
using SPay.Repository.Enum;


namespace SPay.Repository
{
    public interface IStoreRepository
    {
		Task<IList<Store>> GetListStoreAsync(GetListStoreRequest request);
		Task<Store> GetStoreByKeyAsync(string key);
		Task<bool> DeleteStoreAsync(Store storeExisted);
		Task<bool> CreateStoreAsync(Store item);
		Task<bool> UpdateStoreAsync(string key, Store updatedStore);
	}
	public class StoreRepository : IStoreRepository
    {
		private readonly SpayDBContext _context;

		public StoreRepository(SpayDBContext context)
		{
			_context = context;
		}

		public async Task<bool> CreateStoreAsync(Store item)
		{
			_context.Stores.Add(item);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> DeleteStoreAsync(Store storeCategoryExisted)
		{
			storeCategoryExisted.Status = (byte)BasicStatusEnum.Deleted;
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<IList<Store>> GetListStoreAsync(GetListStoreRequest request)
		{
			var query = _context.Stores
				.Where(pp => !pp.Status.Equals((byte)BasicStatusEnum.Deleted))
				.Include(s => s.UserKeyNavigation)
				.Include(s => s.WalletKeyNavigation)
				.Include(s => s.StoreCateKeyNavigation)
				.OrderByDescending(s => s.InsDate)
				.AsQueryable();
			if (!string.IsNullOrEmpty(request.StoreName))
			{
				query = query.Where(s => s.StoreName.Contains(request.StoreName));
			}

			if (!string.IsNullOrEmpty(request.StoreCateKey))
			{
				query = query.Where(s => s.StoreCateKey.Contains(request.StoreCateKey));
			}

			if (!string.IsNullOrEmpty(request.OwnerName))
			{
				query = query.Where(s => s.UserKeyNavigation.Fullname.Contains(request.OwnerName));
			}

			if (!string.IsNullOrEmpty(request.OwnerNumberPhone))
			{
				query = query.Where(s => s.UserKeyNavigation.PhoneNumber.Contains(request.OwnerNumberPhone));
			}

			return await query.ToListAsync();
		}

		public async Task<Store> GetStoreByKeyAsync(string key)
		{
			var response = await _context.Stores.SingleOrDefaultAsync(
											s => s.StoreKey.Equals(key)
											&& !s.Status.Equals((byte)BasicStatusEnum.Deleted));
			if (response == null)
			{
				throw new Exception($"Store with key '{key}' not found.");
			}
			return response;
		}

		public async Task<bool> UpdateStoreAsync(string key, Store updatedStore)
		{
			var existedStore = await _context.Stores.SingleOrDefaultAsync(s => s.StoreKey.Equals(key));
			if (existedStore == null)
			{
				return false;
			}
			existedStore.StoreName = updatedStore.StoreName;
			existedStore.Description = updatedStore.Description;
			if (await _context.SaveChangesAsync() <= 0)
			{
				throw new Exception($"Nothing is update!");
			}
			return true;
		}
	}
}
