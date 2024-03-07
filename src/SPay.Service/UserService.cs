using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Admin.User;
using SPay.Repository;
using SPay.Repository.Enum;
using SPay.Service.Utils;

namespace SPay.Service
{
	public interface IUserService
	{
		//Task<bool> Login(string phoneNumber, string password);
		Task<bool> CreateUserAsync(CreateUserModel model);
	}
	public class UserService : IUserService
	{
		private readonly IUserRepository _repo;
        public UserService(IUserRepository _repo)
        {
            this._repo = _repo;
        }
        public async Task<bool> CreateUserAsync(CreateUserModel model)
		{
			var user = new User
			{
				UserKey = model.UserKey,
				Username = model.NumberPhone,
				Password = model.Password,
				Fullname = model.FullName,
				Role = model.Role,
				Status = (byte)AccountStatusEnum.Active,
				InsDate = DateTimeHelper.GetDateTimeNow(),
			};
			return await _repo.CreateUserAsync(user);
		}
	}
}
