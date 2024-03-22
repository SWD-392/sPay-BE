using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Store.Request
{
    public class GetListStoreRequest : PagingRequest
    {
		public string? UserKey { get; set; } = null;
		public string? StoreName { get; set; } = null;
		public string? OwnerName { get; set; } = null;
		public string? OwnerNumberPhone { get; set; } = null;
		public string? StoreCateKey { get; set; } = null;
	}
}
