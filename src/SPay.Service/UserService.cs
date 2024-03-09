using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Admin.User;
using SPay.BO.DTOs.Auth.Request;
using SPay.BO.DTOs.Auth.Response;
using SPay.Repository;
using SPay.Repository.Enum;
using SPay.Service.Utils;

namespace SPay.Service
{
	public interface IUserService
	{
		//public Task<PaginatedList<GetAccountResponse>> GetAllAccounts(int page, int size);
		//public void CreateAccount(CreateAccountRequest createAccountRequest);
		//public Task<UpdateAccountResponse> UpdateAccountInformation(int id, UpdateAccountRequest updateAccountRequest);
		//public Task<bool> DeleteAccountStatus(int id);
		public Task<LoginResponse> Login(LoginRequest loginRequest);
		public Task<LoginResponse> SignUp(SignUpRequest signUpRequest);
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
				Status = (byte)UserStatusEnum.Active,
				InsDate = DateTimeHelper.GetDateTimeNow(),
			};
			return await _repo.CreateUserAsync(user);
		}

		public Task<bool> DeleteAccountStatus(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<LoginResponse> Login(LoginRequest loginRequest)
		{
			var userLogin = await _repo.LoginAsync(loginRequest);
			LoginResponse response = new LoginResponse
			{
				UserKey = userLogin.UserKey,
				PhoneNumber = userLogin.Username,
				Role = userLogin.Role,
				Status = userLogin.Status
			};
			var token = GenerateJwtToken(userLogin);
			response.AccessToken = token;
			return response;
		}

		public async Task<LoginResponse> SignUp(SignUpRequest request)
		{
			var checkUser = _repo.GetUserByPhoneAsync(request.Phone);
			if(checkUser != null)
			{
				return null;
			}
			return null;
		}

		private string GenerateJwtToken(User user)
		{
			IConfiguration config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true)
				.Build();

			var key = config["Jwt:Key"];
			var issuer = config["Jwt:Issuer"];
			var audience = config["Jwt:Audience"];

			JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
			SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
			var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

			List<Claim> claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Jti, user.UserKey),
				new Claim(JwtRegisteredClaimNames.Sub, user.Fullname.ToString()),
				new Claim(ClaimTypes.Role, user.Role.ToString())
			};

			//Add expiredTime of token
			var expires = DateTime.Now.AddMinutes(30);

			//Create token
			var token = new JwtSecurityToken(issuer, audience, claims, notBefore: DateTime.Now, expires, credentials);
			return jwtHandler.WriteToken(token);
		}
	}
}
