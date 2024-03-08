using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Admin.Customer.RequestModel
{
	public class CreateCustomerRequest
	{
		public string FullName { get; set; } = null!;
		public string? Email { get; set; }
		public string? Address { get; set; }
		public string PhoneNumber { get; set; } = null!;
		public string Password { get; set; } = null!;
	}
}
