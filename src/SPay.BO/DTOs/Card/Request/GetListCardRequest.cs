using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Card.Request
{
    public class GetListCardRequest : PagingRequest
    {
        public string? CardTypeKey { get; set; } = string.Empty;
		public string? PromotionPackageKey { get; set; } = string.Empty;	}
}
