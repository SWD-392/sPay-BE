using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Auth.Request;
using SPay.BO.DTOs.Auth.Response;
using SPay.BO.DTOs.User.Request;
using SPay.Repository.Enum;

namespace SPay.Repository
{
	public interface IUserRepository
	{
		Task<User> LoginAsync(LoginRequest request);
		Task<IList<User>> GetListUserAsync(GetListUserRequest request, bool isStore = false);
		Task<User> GetUserByKeyAsync(string key, bool isStore = false);
		Task<User> GetUserByKeyForTransactionAsync(string key);
		Task<bool> DeleteUserAsync(User UserExisted);
		Task<bool> CreateUserAsync(User item, bool isStore = false);
		Task<bool> UpdateUserAsync(string key, User updatedUser);
	}
	public class UserRepository : IUserRepository
	{

		private readonly SpayDBContext _context;
		private readonly IMembershipRepository _memRepo;
		public UserRepository(SpayDBContext _context, IMembershipRepository _memRepo)
		{
			this._context = _context;
			this._memRepo = _memRepo;
		}

		public async Task<bool> CreateUserAsync(User item, bool isStore = false)
		{
			try
			{
				if (!isStore)
				{
					var defaultMembership = new Membership();
					defaultMembership.MembershipKey = string.Format("{0}{1}", PrefixKeyConstant.MEMBERSHIP, Guid.NewGuid().ToString().ToUpper());
					defaultMembership.CardKey = null;
					defaultMembership.UserKey = item.UserKey;

					// Thực hiện tạo membership trong transaction
					bool membershipCreated = await _memRepo.CreateMembershipAsync(defaultMembership);
				}

				var roleKey = await GetRoleKeyAsync(isStore);
				if (roleKey == null)
				{
					return false;
				}

				item.RoleKey = roleKey;
				_context.Users.Add(item);

				// Trả về true nếu thành công
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("Error creating user.", ex);
			}
		}



		public async Task<bool> DeleteUserAsync(User userCategoryExisted)
		{
			userCategoryExisted.Status = (byte)BasicStatusEnum.Deleted;
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<IList<User>> GetListUserAsync(GetListUserRequest request, bool isStore = false)
		{
			var roleKey = await GetRoleKeyAsync(isStore);
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

		public async Task<User> GetUserByKeyAsync(string key, bool isStore = false)
		{
			var roleKey = await GetRoleKeyAsync(isStore);
			var response = await _context.Users.SingleOrDefaultAsync(
											u => u.UserKey.Equals(key)
											&& u.RoleKey.Equals(roleKey)
											&& !u.Status.Equals((byte)BasicStatusEnum.Deleted));
			if (response == null)
			{
				throw new Exception($"User with key '{key}' not found.");
			}
			return response;
		}

		public async Task<bool> UpdateUserAsync(string key, User updatedUser)
		{
			var existedUser = await _context.Users.SingleOrDefaultAsync(u => u.UserKey.Equals(key));
			if (existedUser == null)
			{
				return false;
			}
			if (!string.IsNullOrEmpty(updatedUser.Fullname))
			{
				existedUser.Fullname = updatedUser.Fullname;
			}
			if (!string.IsNullOrEmpty(updatedUser.Password))
			{
				existedUser.Password = updatedUser.Password;
			}
			if (await _context.SaveChangesAsync() <= 0)
			{
				throw new Exception($"Nothing is update!");
			}
			return true;
		}

		private async Task<string> GetRoleKeyAsync(bool isStore)
		{
			var roleQuery = _context.Roles.AsQueryable();
			if (isStore)
			{
				roleQuery = roleQuery.Where(r => r.RoleName.Equals(Constant.Role.STORE_ROLE));
			}
			else
			{
				roleQuery = roleQuery.Where(r => r.RoleName.Equals(Constant.Role.CUSTOMER_ROLE));
			}

			var role = await roleQuery.SingleOrDefaultAsync();
			if (role != null)
			{
				return role.RoleKey;
			}
			else
			{
				return string.Empty;
			}
		}
		public async Task<User> LoginAsync(LoginRequest request)
		{
			var user = await _context.Users.Include(u => u.RoleKeyNavigation)
				.SingleOrDefaultAsync(u =>
				u.PhoneNumber.Equals(request.PhoneNumber) &&
				u.Password.Equals(request.Password) &&
				u.Status == (byte)UserStatusEnum.Active);
			if (user == null)
			{
				throw new Exception($"Incorrect username or password.");
			}
			return user; ;
		}

		public async Task<User> GetUserByKeyForTransactionAsync(string key)
		{
			var response = await _context.Users.SingleOrDefaultAsync(
											u => u.UserKey.Equals(key));
			if (response == null)
			{
				throw new Exception($"User with key '{key}' not found.");
			}
			return response;
		}

		#region Comment ref
		//      public async Task<bool> CreateUserAsync(User user)
		//{
		//	_context.Users.Add(user);
		//	return await _context.SaveChangesAsync() > 0;
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
