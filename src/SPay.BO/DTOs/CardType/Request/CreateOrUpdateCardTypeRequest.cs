using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.CardType.Request
{
	public class CreateOrUpdateCardTypeRequest
	{
		public string CardTypeName { get; set; } = null!;
		public string? Description { get; set; }
		public string StoreCateKey { get; set; } = null!;
	}
}
