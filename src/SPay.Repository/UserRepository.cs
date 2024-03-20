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
using SPay.BO.DTOs.User.Request;
using SPay.BO.DTOs.User.Request;
using SPay.Repository.Enum;

namespace SPay.Repository
{
	public interface IUserRepository
	{
		//Task<User> LoginAsync(LoginRequest request);
		Task<IList<User>> GetListUserAsync(GetListUserRequest request, bool isStore = false);
		Task<User> GetUserByKeyAsync(string key);
		Task<bool> DeleteUserAsync(User UserExisted);
		Task<bool> CreateUserAsync(User item, bool isStore = false);
		Task<bool> UpdateUserAsync(string key, User updatedUser);
	}
	public class UserRepository : IUserRepository
	{
		private const string CUSTOMER_ROLE = "Customer";
		private const string STORE_ROLE = "Customer";

		private readonly SpayDBContext _context;
        public UserRepository(SpayDBContext _context)
        {
            this._context = _context;
        }

		public async Task<bool> CreateUserAsync(User item ,bool isStore = false)
		{

			var roleKey = await GetRoleKeyAsync(isStore);
			if (roleKey != null)
			{
				item.RoleKey = roleKey;
				_context.Users.Add(item);
				return await _context.SaveChangesAsync() > 0;
			}
			else
			{
				return false;
			}
		}

		public async Task<bool> DeleteUserAsync(User userCategoryExisted)
		{
			userCategoryExisted.Status = (byte)BasicStatusEnum.Deleted;
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<IList<User>> GetListUserAsync(GetListUserRequest request, bool isStore = false)
		{
			var roleKey = GetRoleKeyAsync(isStore);
			var query = _context.Users
				.Where(u => !u.Status.Equals((byte)BasicStatusEnum.Deleted)
							&& u.RoleKey.Equals(roleKey))
				.AsQueryable();
			if (!string.IsNullOrEmpty(request.Name))
			{
				query = query.Where(u => u.Fullname.Contains(request.Name));
			}

			if (!string.IsNullOrEmpty(request.PhoneNumber))
			{
				query = query.Where(u => u.PhoneNumber.Contains(request.PhoneNumber));
			}

			return await query.ToListAsync();
		}

		public async Task<User> GetUserByKeyAsync(string key)
		{
			var response = await _context.Users.SingleOrDefaultAsync(
											u => u.UserKey.Equals(key)
											&& !u.Status.Equals((byte)BasicStatusEnum.Deleted));

			return response ?? new User();
		}

		public async Task<bool> UpdateUserAsync(string key, User updatedUser)
		{
			var existedUser = await _context.Users.SingleOrDefaultAsync(u => u.UserKey.Equals(key));
			if (existedUser == null)
			{
				return false;
			}

			existedUser.Fullname = updatedUser.Fullname;
			existedUser.Password = updatedUser.Password;
			existedUser.PhoneNumber = updatedUser.PhoneNumber;
			return await _context.SaveChangesAsync() > 0;
		}

		private async Task<string> GetRoleKeyAsync(bool isStore)
		{
			var roleQuery = _context.Roles.AsQueryable();
			if (isStore)
			{
				roleQuery = roleQuery.Where(r => r.RoleName.Equals(STORE_ROLE));
			}
			else
			{
				roleQuery = roleQuery.Where(r => r.RoleName.Equals(CUSTOMER_ROLE));
			}

			var role = await roleQuery.FirstOrDefaultAsync();
			if (role != null)
			{
				return role.RoleKey;
			}
			else
			{
				return string.Empty;
			}
		}


		#region Comment ref
		//      public async Task<bool> CreateUserAsync(User user)
		//{
		//	_context.Users.Add(user);
		//	return await _context.SaveChangesAsync() > 0;
		//}

		//public async Task<User> LoginAsync(LoginRequest request)
		//{
		//	var user = await _context.Users.SingleOrDefaultAsync(u =>
		//		u.Username.Equals(request.PhoneNumber) &&
		//		u.Password.Equals(request.Password) &&
		//		u.Status == (byte)UserStatusEnum.Active);
		//	return user ?? new User();
		//}


		//public async Task<User> GetUserByPhoneAsync(string phoneNumber, int role)
		//{
		//	var user = await _context.Users.FirstOrDefaultAsync(c => c.Username.Equals(phoneNumber));
		//	return user ?? new User();
		//}

		//public async Task<User> SignUpAsync(LoginRequest request)
		//{
		//	var user = await _context.Users.FirstOrDefaultAsync(c => c.Username.Equals(request.PhoneNumber));

		//	throw new NotImplementedException();
		//}

		//public Task<User> SignUpAsync(SignUpRequest request)
		//{
		//	throw new NotImplementedException();
		//} 
		#endregion
	}
}
