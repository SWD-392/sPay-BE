using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Store.Response
{
    public class StoreResponse
    {
        public int No { get; set; }
		public string StoreKey { get; set; } = null!;
		public string UserKey { get; set; } = null!;
		public string StoreName { get; set; } = null!;
		public string? Description { get; set; }
		public string StoreCateKey { get; set; } = null!;
		public string WalletKey { get; set; } = null!;
	}
}
