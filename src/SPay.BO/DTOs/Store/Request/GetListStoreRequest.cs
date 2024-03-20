using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Store.Request
{
    public class GetListStoreRequest : PagingRequest
    {
        public string? Name { get; set; } = null;
    }
}
