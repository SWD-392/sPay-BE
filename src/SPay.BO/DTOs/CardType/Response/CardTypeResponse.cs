using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.CardType.Response
{
	public class CardTypeResponse 
	{
		public int No { get; set; } = 0;
		public string CardTypeKey { get; set; } = null!;
		public string? CardTypeName { get; set; }
		public string? Description { get; set; }
		public int TotalCardUse { get; set; }
	}
}
