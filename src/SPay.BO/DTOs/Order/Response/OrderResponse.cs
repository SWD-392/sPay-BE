using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Order.Response
{
	public class OrderResponse
	{
		public int No { get; set; } = 0;
		public string OrderKey { get; set; } = null!;
		public string FromUserName { get; set; } = null!;
		public string ByCardName { get; set; }
		public string ToStoreName { get; set; } = null!;
		public string StoreCateName { get; set; } = null!;
		public string? Description { get; set; }
		public decimal? TotalAmount { get; set; }
		public string Status { get; set; } = null!;
	}
}
