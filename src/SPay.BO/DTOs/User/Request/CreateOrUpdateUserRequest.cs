using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.User.Request
{
	public class CreateOrUpdateUserRequest
	{
		public string Password { get; set; } = null!;
		public string Fullname { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
	}
}
