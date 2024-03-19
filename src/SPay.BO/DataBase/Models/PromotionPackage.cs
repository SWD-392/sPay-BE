using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class PromotionPackage
    {
        public PromotionPackage()
        {
            Cards = new HashSet<Card>();
        }

        public string PromotionPackageKey { get; set; } = null!;
        public string PackageName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal ValueUsed { get; set; }
        public byte DiscountPercentage { get; set; }
        public decimal? Price { get; set; }
        public byte NumberDate { get; set; }
        public bool WithdrawAllowed { get; set; }
        public byte Status { get; set; }
        public DateTime InsDate { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
