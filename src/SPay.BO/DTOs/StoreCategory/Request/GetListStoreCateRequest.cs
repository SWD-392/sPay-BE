using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.StoreCategory.Request
{
	public class GetListStoreCateRequest : PagingRequest
	{
		public string? Name { get; set; } = null;

	}
}
