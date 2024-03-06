using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs
{
	public class PagingRequest
	{
		public int PageIndex { get; set; } = 1;
		public int PageSize { get; set; } = 100;
	}
}
