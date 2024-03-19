using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.StoreCategory.Request
{
	public class CreateOrUpdateStoreCateRequest
	{
		public string CategoryName { get; set; } = null!;
		public string? Description { get; set; }
	}
}
