using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Admin.Wallet
{
	public class CreateWalletModel
	{
		public string WalletKey { get; set; } = null!;
		public string WalletTypeKey { get; set; } = null!;
		public string? CardKey { get; set; } = null;
		public string? StoreKey { get; set; } = null;
		public string? CustomerKey { get; set; } = null;
		public decimal? Balance { get; set; } = 0; 
	}
}
