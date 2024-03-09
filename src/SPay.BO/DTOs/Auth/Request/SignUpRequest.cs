using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Auth.Request
{
	public class SignUpRequest
	{
		[Required(ErrorMessage = "FullName is required")]
		public string FullName { get; set; }
		[Required(ErrorMessage = "Password is required")]
		public string Phone { get; set; }
		public string Password { get; set; }
	}
}
