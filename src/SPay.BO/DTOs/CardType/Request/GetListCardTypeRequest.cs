using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.CardType.Request
{
	public class GetListCardTypeRequest : PagingRequest
	{
		public string? CardTypeName { get; set; } = string.Empty;
		public string? StoreCateKey { get; set; } = string.Empty;
	}
}
