using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
			CreateMap< User, LoginResponse > ()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.RoleKeyNavigation.RoleName));
		}
	}
}
