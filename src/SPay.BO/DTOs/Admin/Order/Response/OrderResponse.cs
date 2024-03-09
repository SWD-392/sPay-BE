using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Admin.Order.Response
{
	public class OrderResponse
	{
		public int? No { get; set; } = 0;
		public string OrderKey { get; set; } = null!;
		public string? CustomerKey { get; set; }
		public string FromCustomer { get; set; }
		public string StoreKey { get; set; } = null!;
		public string ToStore { get; set; }
		public string CardKey { get; set; } = null!;
		public string CardName { get; set; }
		public string? OrderDescription { get; set; }
		public decimal? Value { get; set; }
		public DateTime? Date { get; set; }
		public byte Status { get; set; }
	}
}
