using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Auth.Response
{
	public class LoginResponse
	{
		public string AccessToken { get; set; }
		public string UserKey { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }
		public string FullName { get; set; }
		public byte Role { get; set; }
		public byte Status { get; set; }
	}
}
