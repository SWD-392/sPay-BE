using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Admin.Order.Request
{
	public class CreateOrderRequest
	{
		public string OrderKey { get; set; } = null!;
		public string? CustomerKey { get; set; }
		public string StoreKey { get; set; } = null!;
		public string CardKey { get; set; } = null!;
		public string? OrderDescription { get; set; }
		public decimal? Value { get; set; }
	}
}
