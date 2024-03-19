using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Store.Request
{
    public class CreateOrUpdateStoreRequest
    {
		public string UserKey { get; set; } = null!;
		public string StoreName { get; set; } = null!;
		public string? Description { get; set; }
		public string StoreCateKey { get; set; } = null!;
		public string WalletKey { get; set; } = null!;
	}
}
