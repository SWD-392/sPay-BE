using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Auth.Request;
using SPay.BO.DTOs.Auth.Response;
using SPay.Repository.Enum;

namespace SPay.Repository
{
	public interface IUserRepository
	{
		Task<bool> CreateUserAsync(User user);
		Task<User> LoginAsync(LoginRequest request);
		Task<User> SignUpAsync(SignUpRequest request);
		Task<User> GetUserByPhoneAsync(string phoneNumber);
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

		public async Task<User> LoginAsync(LoginRequest request)
		{
			var user = await _context.Users.SingleOrDefaultAsync(u =>
				u.Username.Equals(request.PhoneNumber) &&
				u.Password.Equals(request.Password) &&
				u.Status == (byte)UserStatusEnum.Active);
			return user ?? new User();
		}


		public async Task<User> GetUserByPhoneAsync(string phoneNumber)
		{
			var user = await _context.Users.FirstOrDefaultAsync(c => c.Username.Equals(phoneNumber));
			return user ?? new User();
		}

		public async Task<User> SignUpAsync(LoginRequest request)
		{
			var user = await _context.Users.FirstOrDefaultAsync(c => c.Username.Equals(request.PhoneNumber));

			throw new NotImplementedException();
		}

		public Task<User> SignUpAsync(SignUpRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
