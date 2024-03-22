using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Order.Request
{
	public class GetListOrderRequest : PagingRequest
	{
		public string MembershipKey { get; set; } = null!;
		public string StoreKey { get; set; } = null!;
		public string? Description { get; set; }
		public decimal? TotalAmount { get; set; }
	}
}
