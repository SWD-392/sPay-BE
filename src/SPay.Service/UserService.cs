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
		Task<User> GetUserByPhoneAsync(string phoneNumber, int role);
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
		public async Task<bool> CreateUserAsync(CreateUserModel model)
		{
			var user = _mapper.Map<User>(model);
			user.RoleKey = model.RoleKey;
			user.Status = (byte)UserStatusEnum.Active;
			user.InsDate = DateTimeHelper.GetDateTimeNow();
			return await _repo.CreateUserAsync(user);
		}

		public Task<bool> DeleteAccountStatus(int id)
		{
			throw new NotImplementedException();
		}

		public Task<User> GetUserByPhoneAsync(string phoneNumber, int role)
		{
			return _repo.GetUserByPhoneAsync(phoneNumber, role);
		}

		public async Task<LoginResponse> Login(LoginRequest loginRequest)
		{
			var userLogin = await _repo.LoginAsync(loginRequest);
			var response = _mapper.Map<LoginResponse>(userLogin); ;
			var token = GenerateJwtToken(userLogin);
			response.AccessToken = token;
			return response;
		}

		public async Task<LoginResponse> SignUp(SignUpRequest request)
		{
			var checkUser = await _repo.GetUserByPhoneAsync(request.Phone, 0);
			if(checkUser != null)
			{
				return new LoginResponse();
			}
			return new LoginResponse();
		}

		private string GenerateJwtToken(User user)
		{
			IConfiguration config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true)
				.Build();

			var key = config["Jwt:SecretKey"];
			var issuer = config["Jwt:Issuer"];
			var audience = config["Jwt:Audience"];

			JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
			SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
			var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

			List<Claim> claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Jti, user.UserKey),
				new Claim(JwtRegisteredClaimNames.Sub, user.Fullname.ToString()),
				new Claim(ClaimTypes.Role, user.RoleKeyNavigation.RoleName.ToString())
			};

			//Add expiredTime of token
			var expires = DateTime.Now.AddMinutes(30);

			//Create token
			var token = new JwtSecurityToken(issuer, audience, claims, notBefore: DateTime.Now, expires, credentials);
			return jwtHandler.WriteToken(token);
		}
	}
}
