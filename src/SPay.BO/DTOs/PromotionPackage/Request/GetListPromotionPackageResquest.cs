using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.PromotionPackage.Request
{
	public class GetListPromotionPackageResquest : PagingRequest
	{
		public string? Name { get; set; } = null;
		public byte NumberDate { get; set; }
		public bool WithdrawAllowed { get; set; }
	}
}
