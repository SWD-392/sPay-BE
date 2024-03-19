using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Admin.User
{
	public class CreateUserModel
	{
		public string UserKey { get; set; } = null!;
		public string NumberPhone { get; set; }
		public string Password { get; set; }
		public string RoleKey { get; set; }
		public string FullName { get; set; }
	}
}
