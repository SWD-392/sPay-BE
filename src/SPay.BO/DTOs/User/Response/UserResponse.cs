using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.User.Response
{
	public class UserResponse
	{
		public int No { get; set; } = 0;
		public string UserKey { get; set; } = null!;
		public string ZaloId { get; set; } = null!;
		public string Fullname { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
	}
}
