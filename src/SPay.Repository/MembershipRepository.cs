using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Membership.Request;
using SPay.Repository.Enum;

namespace SPay.Repository
{
	public interface IMembershipRepository
	{
		Task<IList<Membership>> GetListMembershipAsync(GetListMembershipRequest request);
		Task<Membership> GetMembershipByKeyAsync(string key);
		Task<bool> CreateMembershipAsync(Membership item);
	}
	public class MembershipRepository : IMembershipRepository
	{
		private readonly SpayDBContext _context;

		public MembershipRepository(SpayDBContext context)
		{
			_context = context;
		}

		public async Task<IList<Membership>> GetListMembershipAsync( GetListMembershipRequest request)
		{
			var response = await _context.Memberships
				.ToListAsync();
			return response;
		}

		public async Task<Membership> GetMembershipByKeyAsync(string key)
		{
			var response = await _context.Memberships.SingleOrDefaultAsync(
											pp => pp.MembershipKey.Equals(key));
			return response ?? new Membership();
		}

		public async Task<bool> CreateMembershipAsync(Membership item)
		{
			_context.Memberships.Add(item);
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
