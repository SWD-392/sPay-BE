using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Admin.Customer.ResponseModel
{
    public class CustomerResponse
    {
		public string CustomerKey { get; set; } = null!;
		public string UserKey { get; set; } = null!;
		public string? Email { get; set; }
		public string? Address { get; set; }
		public string? CreateBy { get; set; }
		public string? CustomerName { get; set; }
		public byte Status { get; set; }
	}
}
