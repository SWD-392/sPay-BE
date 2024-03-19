using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;

namespace SPay.Repository
{
	public interface IRoleRepository
	{
		Task<IList<Role>> GetListRoleAsync();
	}
	public class RoleRepository : IRoleRepository
	{
		private readonly SpayDBContext _context;

		public RoleRepository(SpayDBContext context)
		{
			_context = context;
		}

		public async Task<IList<Role>> GetListRoleAsync()
		{
			var response = await _context.Roles.ToListAsync();
			return response;
		}
	}
}
