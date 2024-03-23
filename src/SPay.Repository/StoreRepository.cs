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
		Task<Store> GetStoreByKeyForTransactionAsync(string key);
		Task<bool> DeleteStoreAsync(Store storeExisted);
		Task<bool> CreateStoreAsync(Store item);
		Task<bool> UpdateStoreAsync(string key, Store updatedStore);
		Task<string> GetStoreKeyByUserKey(string phoneNumber);
	}
	public class StoreRepository : IStoreRepository
    {
		private readonly SpayDBContext _context;

		public StoreRepository(SpayDBContext context)
		{
			_context = context;
		}

		public async Task<string> GetStoreKeyByUserKey(string phoneNumber)
		{
			var store = await _context.Stores.SingleOrDefaultAsync(s => s.UserKeyNavigation.PhoneNumber.Equals(phoneNumber));
			if (store == null)
			{
				throw new Exception($"Store with phoneNumber: {phoneNumber} not found!");
			}
			return store.StoreKey;
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

			if (!string.IsNullOrEmpty(request.UserKey))
			{
				query = query.Where(s => s.UserKey.Contains(request.UserKey));
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
			var response = await _context.Stores
										.Include(s => s.UserKeyNavigation)
										.Include(s => s.WalletKeyNavigation)
										.Include(s => s.StoreCateKeyNavigation)
										.SingleOrDefaultAsync(s => s.StoreKey.Equals(key)
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
			if (!string.IsNullOrEmpty(updatedStore.StoreName))
			{
				existedStore.StoreName = updatedStore.StoreName;

			}
			if (!string.IsNullOrEmpty(updatedStore.Description))
			{
				existedStore.Description = updatedStore.Description;
			}

			if (!string.IsNullOrEmpty(updatedStore.StoreCateKey))
			{
				existedStore.StoreCateKey = updatedStore.StoreCateKey;
			}
			if (!(await _context.SaveChangesAsync() > 0))
			{
				throw new Exception($"Nothing is update!");
			}
			return true;
		}

		public async Task<Store> GetStoreByKeyForTransactionAsync(string key)
		{
			var response = await _context.Stores
							.Include(s => s.UserKeyNavigation)
							.Include(s => s.WalletKeyNavigation)
							.Include(s => s.StoreCateKeyNavigation)
							.SingleOrDefaultAsync(s => s.StoreKey.Equals(key));
			if (response == null)
			{
				throw new Exception($"Store with key '{key}' not found.");
			}
			return response;
		}
	}
}
