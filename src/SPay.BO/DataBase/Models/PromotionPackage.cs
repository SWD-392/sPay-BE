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
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte Status { get; set; }
        public DateTime InsDate { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
