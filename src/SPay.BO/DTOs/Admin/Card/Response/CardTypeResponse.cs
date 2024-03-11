using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Admin.Card.Response
{
	public class CardTypeResponse
	{
		public string CardTypeKey { get; set; } = null!;
		public string? CardTypeName { get; set; }
		public string? TypeDescription { get; set; }
		public bool? WithdrawalAllowed { get; set; }
	}
}
