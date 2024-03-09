using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Admin.Order.Request
{
	public class OrderSearchRequest : PagingRequest
	{
		public string? FromCustomer { get; set; }
		public string? ToStore { get; set; }
		public string? CardName { get; set; }
	}
}
