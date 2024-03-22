using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Auth.Request
{
	public class LoginRequest
	{
		[Required(ErrorMessage = "Phone is required")]
		public string PhoneNumber { get; set; } = null!;
		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; } = null!;
	}
}
