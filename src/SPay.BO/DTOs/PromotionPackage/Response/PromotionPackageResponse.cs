using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.PromotionPackage.Response
{
	public class PromotionPackageResponse
	{
		public int No { get; set; } = 0;
		public string PromotionPackageKey { get; set; } = null!;
		public string? Name { get; set; }
		public string? Description { get; set; }
		public decimal? ValueUsed { get; set; }
		public byte DiscountPercentage { get; set; }
		public decimal? Price { get; set; }
	}
}
