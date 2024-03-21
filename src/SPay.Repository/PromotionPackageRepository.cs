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
	public interface IPromotionPackageRepository
	{
		Task<IList<PromotionPackage>> GetListPromotionPackageAsync();
		Task<PromotionPackage> GetPromotionPackageByKeyAsync(string key);
		Task<bool> DeletePromotionPackageAsync(PromotionPackage item);
		Task<bool> CreatePromotionPackageAsync(PromotionPackage item);
		Task<bool> UpdatePromotionPackageAsync(string key, PromotionPackage updatedPackage);
	}
	public class PromotionPackageRepository : IPromotionPackageRepository
	{
		private readonly SpayDBContext _context;

		public PromotionPackageRepository(SpayDBContext context)
		{
			_context = context;
		}

		public async Task<IList<PromotionPackage>> GetListPromotionPackageAsync()
		{
			var response = await _context.PromotionPackages
				.Where(pp => !pp.Status.Equals((byte)BasicStatusEnum.Deleted))
				.OrderByDescending(pp => pp.InsDate)
				.ToListAsync();
			return response;
		}

		public async Task<PromotionPackage> GetPromotionPackageByKeyAsync(string key)
		{
			var response = await _context.PromotionPackages.SingleOrDefaultAsync(
											pp => pp.PromotionPackageKey.Equals(key)
											&& !pp.Status.Equals((byte)BasicStatusEnum.Deleted));

			return response ?? new PromotionPackage();
		}

		public async Task<bool> DeletePromotionPackageAsync(PromotionPackage packageExisted)
		{
			packageExisted.Status = (byte)BasicStatusEnum.Deleted;
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> CreatePromotionPackageAsync(PromotionPackage item)
		{
			_context.PromotionPackages.Add(item);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> UpdatePromotionPackageAsync(string key, PromotionPackage updatedPackage)
		{
			var existedPackage = await _context.PromotionPackages.SingleOrDefaultAsync(p => p.PromotionPackageKey.Equals(key));
			if (existedPackage == null)
			{
				return false;
			}

			existedPackage.PackageName = updatedPackage.PackageName;
			existedPackage.Description = updatedPackage.Description;
			existedPackage.DiscountPercentage = updatedPackage.DiscountPercentage;
			existedPackage.UsaebleAmount = updatedPackage.UsaebleAmount;
			existedPackage.Price = updatedPackage.Price;
			existedPackage.WithdrawAllowed = updatedPackage.WithdrawAllowed;
			existedPackage.NumberDate = updatedPackage.NumberDate;
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
