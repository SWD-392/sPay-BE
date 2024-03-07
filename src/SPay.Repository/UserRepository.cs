using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPay.BO.DataBase.Models;

namespace SPay.Repository
{
	public interface IUserRepository
	{
		Task<bool> CreateUserAsync(User user);
	}
	public class UserRepository : IUserRepository
	{
		private readonly SPayDbContext _context;
        public UserRepository(SPayDbContext _context)
        {
            this._context = _context;
        }
        public async Task<bool> CreateUserAsync(User user)
		{
			_context.Users.Add(user);
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
