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
	public interface IStoreCategoryRepository
	{
		Task<IList<StoreCategory>> GetListStoreCategoryAsync();
		Task<StoreCategory> GetStoreCategoryByKeyAsync(string key);
		Task<bool> DeleteStoreCategoryAsync(StoreCategory storeCategoryExisted);
		Task<bool> CreateStoreCategoryAsync(StoreCategory item);
		Task<bool> UpdateStoreCategoryAsync(string key, StoreCategory updatedStoreCate);
	}
	public class StoreCategoryRepository : IStoreCategoryRepository
	{
		private readonly SpayDBContext _context;

		public StoreCategoryRepository(SpayDBContext context)
		{
			_context = context;
		}

		public async Task<bool> CreateStoreCategoryAsync(StoreCategory item)
		{
			_context.StoreCategories.Add(item);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> DeleteStoreCategoryAsync(StoreCategory storeCategoryExisted)
		{
			storeCategoryExisted.Status = (byte)BasicStatusEnum.Deleted;
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<IList<StoreCategory>> GetListStoreCategoryAsync()
		{
			var response = await _context.StoreCategories
				.Where(pp => !pp.Status.Equals((byte)BasicStatusEnum.Deleted))
				.ToListAsync();
			return response;
		}

		public async Task<StoreCategory> GetStoreCategoryByKeyAsync(string key)
		{
			var response = await _context.StoreCategories.SingleOrDefaultAsync(
											ct => ct.StoreCategoryKey.Equals(key)
											&& !ct.Status.Equals((byte)BasicStatusEnum.Deleted));

			return response ?? new StoreCategory();
		}

		public async Task<bool> UpdateStoreCategoryAsync(string key, StoreCategory updatedStoreCate)
		{
			var existedStoreCate = await _context.StoreCategories.SingleOrDefaultAsync(p => p.StoreCategoryKey.Equals(key));
			if (existedStoreCate == null)
			{
				return false;
			}

			existedStoreCate.CategoryName = updatedStoreCate.CategoryName;
			existedStoreCate.Description = updatedStoreCate.Description;
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
