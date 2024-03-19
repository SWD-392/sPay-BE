using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Store.Response
{
    public class GetListStoreRequest : PagingRequest
    {
		public string? Name { get; set; } = null;
	}
}
