using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Auth.Response
{
	public class LoginResponse
	{
		public string AccessToken { get; set; } = null!;
		public string UserKey { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
		public string FullName { get; set; } = null!;
		public string Role { get; set; } = null!;
		public string? StoreKey { get; set; } = null;
		public byte Status { get; set; }
	}
}
