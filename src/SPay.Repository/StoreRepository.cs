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
				.AsQueryable();
			if (!string.IsNullOrEmpty(request.Name))
			{
				query = query.Where(p => p.StoreName.Contains(request.Name));
			}
			return await query.ToListAsync();
		}

		public async Task<Store> GetStoreByKeyAsync(string key)
		{
			var response = await _context.Stores.SingleOrDefaultAsync(
											ct => ct.StoreKey.Equals(key)
											&& !ct.Status.Equals((byte)BasicStatusEnum.Deleted));

			return response ?? new Store();
		}

		public async Task<bool> UpdateStoreAsync(string key, Store updatedStore)
		{
			var existedStore = await _context.Stores.SingleOrDefaultAsync(p => p.StoreKey.Equals(key));
			if (existedStore == null)
			{
				return false;
			}

			existedStore.StoreName = updatedStore.StoreName;
			existedStore.Description = updatedStore.Description;
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
