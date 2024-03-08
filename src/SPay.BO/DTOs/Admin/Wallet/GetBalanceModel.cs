using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Admin.Wallet
{
	public class GetBalanceModel
	{
		public string? CardKey { get; set; } = null;
		public string? StoreKey { get; set; } = null;
		public string? CustomerKey { get; set; } = null;
	}
}
