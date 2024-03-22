using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Store.Request
{
	public class UpdateStoreRequest
	{
		public string? Fullname { get; set; } = null;
		public string? Password { get; set; } = null;
		public string? StoreName { get; set; } = null;
		public string? Description { get; set; } = null;
		public string? StoreCateKey { get; set; } = null;
	}
}
