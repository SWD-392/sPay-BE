using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Admin.Store.Request
{
    public class CreateStoreRequest
    {
		public string StoreName { get; set; }
		public string Description { get; set; }
		public string StoreCategory { get; set; }
		public string StoreCategoryKey { get; set; }
		public string OwnerName { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }
		public decimal? Balance { get; set; }
	}
}
