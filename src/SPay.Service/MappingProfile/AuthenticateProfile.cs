using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DataBase.Models;
using SPay.BO.DTOs.Auth.Request;
using SPay.BO.DTOs.Auth.Response;

namespace SPay.Service.MappingProfile
{
	public class AuthenticateProfile : Profile
	{
		public AuthenticateProfile() {
			CreateMap<User, LoginRequest>();
			CreateMap<LoginResponse, User>();
		}
	}
}
