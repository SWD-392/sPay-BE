using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Auth.Request;
using SPay.BO.DTOs.Auth.Response;
using SPay.BO.DTOs.User.Request;
using SPay.BO.DTOs.User.Response;
using SPay.BO.DTOs.User.Request;
using SPay.BO.DTOs.User.Response;
using SPay.BO.Extention.Paginate;
using SPay.Repository;
using SPay.Repository.Enum;
using SPay.Service.Response;
using SPay.Service.Utils;

namespace SPay.Service
{
	public interface IUserService
	{
		//public Task<PaginatedList<GetAccountResponse>> GetAllAccounts(int page, int size);
		//public void CreateAccount(CreateAccountRequest createAccountRequest);
		//public Task<UpdateAccountResponse> UpdateAccountInformation(int id, UpdateAccountRequest updateAccountRequest);
		//public Task<bool> DeleteAccountStatus(int id);
		//public Task<LoginResponse> Login(LoginRequest loginRequest);
		//Task<bool> Login(string phoneNumber, string password);

		Task<SPayResponse<PaginatedList<UserResponse>>> GetListUserAsync(GetListUserRequest request);
		Task<SPayResponse<UserResponse>> GetUserByKeyAsync(string key);
		Task<SPayResponse<bool>> DeleteUserAsync(string key);
		Task<SPayResponse<bool>> CreateUserAsync(CreateOrUpdateUserRequest request);
		Task<SPayResponse<bool>> UpdateUserAsync(string key, CreateOrUpdateUserRequest request);

	}
	public class UserService : IUserService
	{
		private readonly IUserRepository _repo;
		private readonly IMapper _mapper;
		public UserService(IUserRepository _repo, IMapper _mapper)
		{
			this._repo = _repo;
			this._mapper = _mapper;
		}


		public async Task<SPayResponse<bool>> CreateUserAsync(CreateOrUpdateUserRequest request)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				if (request == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Request model is required!");
					return response;
				}
				var createUserInfo = _mapper.Map<User>(request);
				if (createUserInfo == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				createUserInfo.UserKey = string.Format("{0}{1}", PrefixKeyConstant.USER, Guid.NewGuid().ToString().ToUpper());
				createUserInfo.InsDate = DateTimeHelper.GetDateTimeNow();
				createUserInfo.Status = (byte)BasicStatusEnum.Available;
				if (!await _repo.CreateUserAsync(createUserInfo))
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = true;
				response.Success = true;
				response.Message = "Create a User successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<bool>> DeleteUserAsync(string key)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				var existedUser = await _repo.GetUserByKeyAsync(key);

				var success = await _repo.DeleteUserAsync(existedUser);
				if (success == false)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				response.Data = success;
				response.Success = true;
				response.Message = "User delete successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<PaginatedList<UserResponse>>> GetListUserAsync(GetListUserRequest request)
		{
			var response = new SPayResponse<PaginatedList<UserResponse>>();
			try
			{
				var UserCates = await _repo.GetListUserAsync(request);
				if (UserCates.Count <= 0)
				{
					SPayResponseHelper.SetErrorResponse(response, "User has no row in database.");
					return response;
				}
				var res = _mapper.Map<IList<UserResponse>>(UserCates);
				var count = 0;
				foreach (var item in res)
				{
					item.No = ++count;
				}
				response.Data = await res.ToPaginateAsync(request); ;
				response.Success = true;
				response.Message = "Get list User successfully";
				return response;
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<UserResponse>> GetUserByKeyAsync(string key)
		{
			var response = new SPayResponse<UserResponse>();
			try
			{
				var UserCate = await _repo.GetUserByKeyAsync(key);

				var res = _mapper.Map<UserResponse>(UserCate);
				response.Data = res;
				response.Success = true;
				response.Message = "Get User successfully";
				return response;
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		public async Task<SPayResponse<bool>> UpdateUserAsync(string key, CreateOrUpdateUserRequest request)
		{
			SPayResponse<bool> response = new SPayResponse<bool>();
			try
			{
				if (request == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Request model is required!");
					return response;
				}

				var existedUser = await _repo.GetUserByKeyAsync(key);

				var updatedUser = _mapper.Map<User>(request);
				if (updatedUser == null)
				{
					SPayResponseHelper.SetErrorResponse(response, "Something was wrong!");
					return response;
				}
				await _repo.UpdateUserAsync(key, updatedUser);
				response.Data = true;
				response.Success = true;
				response.Message = $"Update the User with key: {key} successfully";
			}
			catch (Exception ex)
			{
				SPayResponseHelper.SetErrorResponse(response, "Error", ex.Message);
			}
			return response;
		}

		#region auth reigoin
		//public async Task<LoginResponse> Login(LoginRequest loginRequest)
		//{
		//	var userLogin = await _repo.LoginAsync(loginRequest);
		//	var response = _mapper.Map<LoginResponse>(userLogin); ;
		//	var token = GenerateJwtToken(userLogin);
		//	response.AccessToken = token;
		//	return response;
		//}

		//private string GenerateJwtToken(User user)
		//{
		//	IConfiguration config = new ConfigurationBuilder()
		//		.SetBasePath(Directory.GetCurrentDirectory())
		//		.AddJsonFile("appsettings.json", true, true)
		//		.Build();

		//	var key = config["Jwt:SecretKey"];
		//	var issuer = config["Jwt:Issuer"];
		//	var audience = config["Jwt:Audience"];

		//	JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
		//	SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
		//	var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

		//	List<Claim> claims = new List<Claim>()
		//	{
		//		new Claim(JwtRegisteredClaimNames.Jti, user.UserKey),
		//		new Claim(JwtRegisteredClaimNames.Sub, user.Fullname.ToString()),
		//		new Claim(ClaimTypes.Role, user.RoleKeyNavigation.RoleName.ToString())
		//	};

		//	//Add expiredTime of token
		//	var expires = DateTime.Now.AddMinutes(30);

		//	//Create token
		//	var token = new JwtSecurityToken(issuer, audience, claims, notBefore: DateTime.Now, expires, credentials);
		//	return jwtHandler.WriteToken(token);
		//} 
		#endregion
	}
}
