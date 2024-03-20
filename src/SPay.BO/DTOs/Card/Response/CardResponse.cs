using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Card.Response
{
    public class CardResponse
    {
        public int No { get; set; }
        public string CardKey { get; set; } = null!;
        public string CardTypeKey { get; set; } = null!;
        public string CardTypeName { get; set; } = null!;
		public string CardNo { get; set; } = null!;
		public string CardName { get; set; } = null!;
        public string Description { get; set; } = null!;
		public string PromotionPackageKey { get; set; } = null!;
		public string PackageName { get; set; } = null!;
		public decimal? ValueUsed { get; set; }
		public byte DiscountPercentage { get; set; }
		public decimal? Price { get; set; }
		public byte NumberDate { get; set; }
		public bool WithdrawAllowed { get; set; }
	}
}
