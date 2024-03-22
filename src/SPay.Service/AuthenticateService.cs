using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Auth.Request;
using SPay.BO.DTOs.Auth.Response;
using SPay.Repository;
using SPay.Repository.Enum;

namespace SPay.Service
{
	public interface IAuthenticateService
	{
		public Task<LoginResponse> Login(LoginRequest loginRequest);
	}

	public class AuthenticateService : IAuthenticateService
	{
		private readonly IUserRepository _repo;
		private readonly IStoreRepository _repoS;
		private readonly IMapper _mapper;

		public AuthenticateService(IUserRepository repo, IStoreRepository repoS, IMapper mapper)
		{
			_repo = repo;
			_repoS = repoS;
			_mapper = mapper;
		}

		public async Task<LoginResponse> Login(LoginRequest loginRequest)
		{
			var userLogin = await _repo.LoginAsync(loginRequest);
			var response = _mapper.Map<LoginResponse>(userLogin);
			if (response.Role.Equals(Constant.Role.STORE_ROLE))
			{
				response.StoreKey = await _repoS.GetStoreKeyByUserKey(response.PhoneNumber);
			}
			var token = GenerateJwtToken(userLogin);
			response.AccessToken = token;
			return response;
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
