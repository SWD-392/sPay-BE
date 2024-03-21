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
		public string PackageName { get; set; } = null!;
		public string? Description { get; set; }
		public decimal? UsaebleAmount { get; set; }
		public byte DiscountPercentage { get; set; }
		public decimal? Price { get; set; }
		public byte NumberDate { get; set; }
		public bool WithdrawAllowed { get; set; }
		public int TotalCardUse { get; set; } = 0;
	}
}
