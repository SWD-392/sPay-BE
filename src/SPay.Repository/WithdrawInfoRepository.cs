using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.WithdrawInfomation.Request;
using SPay.Repository.Enum;
using SPay.Repository.ResponseDTO;

namespace SPay.Repository
{
	public interface IWithdrawInfoRepository
	{
		Task<IList<WithdrawInformation>> GetAllWithdrawInfoTypeAsync(GetListWithdrawInfomationRequest request);
		Task<WithdrawInformation> GetWithdrawInfoByKeyAsync(string key);
		Task<bool> DeleteWithdrawInfoAsync(WithdrawInformation existedWithdrawInfo);
		Task<bool> CreateWithdrawInfoAsync(WithdrawInformation withdrawInfo);
		Task<bool> ProcessMoney(CreateWithdrawInfomationRequest request);
	}
	public class WithdrawInfoRepository : IWithdrawInfoRepository
	{
		private readonly SpayDBContext _context;

		public WithdrawInfoRepository(SpayDBContext context)
		{
			_context = context;
		}

		public async Task<bool> CreateWithdrawInfoAsync(WithdrawInformation withdrawInfo)
		{
			_context.WithdrawInformations.Add(withdrawInfo);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> DeleteWithdrawInfoAsync(WithdrawInformation existedWithdrawInfo)
		{
			existedWithdrawInfo.Status = (byte)WithdrawStatusEnum.Deleted;
			return await _context.SaveChangesAsync() > 0;
		}

		public  async Task<IList<WithdrawInformation>> GetAllWithdrawInfoTypeAsync(GetListWithdrawInfomationRequest request)
		{
			var query = _context.WithdrawInformations
				.Include(w => w.UserKeyNavigation)
				.Include(w => w.UserKeyNavigation.RoleKeyNavigation)
				.Where(o => o.Status != (byte)OrderStatusEnum.Deleted)
				.AsQueryable();

			if (!string.IsNullOrEmpty(request.PhoneNumber))
			{
				query = query.Where(o => o.UserKeyNavigation.PhoneNumber.Contains(request.PhoneNumber));
			}
			if (!string.IsNullOrEmpty(request.UserName))
			{
				query = query.Where(o => o.UserKeyNavigation.Fullname.Equals(request.UserName));
			}
			return await query.ToListAsync();
		}

		public async Task<WithdrawInformation> GetWithdrawInfoByKeyAsync(string key)
		{
			var query = _context.WithdrawInformations
				.Include(w => w.UserKeyNavigation)
				.Include(w => w.UserKeyNavigation.RoleKeyNavigation)
				.Where(o => o.Status != (byte)WithdrawStatusEnum.Deleted && o.WithdrawKey.Equals(key))
				.AsQueryable();

			var withdrawInfo = await query.SingleOrDefaultAsync();
			if (withdrawInfo == null)
			{
				throw new Exception($"Withdraw infomation with key '{key}' not found.");
			}
			return withdrawInfo;
		}

		public async Task<bool> ProcessMoney(CreateWithdrawInfomationRequest request)
		{
			var user = await _context.Stores.Include(s => s.WalletKeyNavigation).SingleOrDefaultAsync(s => s.UserKey.Equals(request.UserKey));
			if (user == null)
			{
				throw new Exception("Process money failed because not found user");
			}
			
			if(request.TotalAmount > user.WalletKeyNavigation.Balance)
			{
				throw new Exception("Cannot withdraw money greater than balance of store");
			}

			user.WalletKeyNavigation.Balance -= request.TotalAmount;
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
