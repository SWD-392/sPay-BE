using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Admin.User
{
	public class CreateUserModel
	{
		public string UserKey { get; set; }
		public string NumberPhone { get; set; }
		public string Password { get; set; }
		public int Role { get; set; } = 2; //Default is Customer
		public string FullName { get; set; }
	}
}
