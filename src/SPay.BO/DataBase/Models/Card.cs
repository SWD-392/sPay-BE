using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Card
    {
        public Card()
        {
            Orders = new HashSet<Order>();
            Wallets = new HashSet<Wallet>();
        }

        public string CardKey { get; set; } = null!;
        public string CardTypeKey { get; set; } = null!;
        public string? CardNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        public byte Status { get; set; }
        public int NumberDate { get; set; }
        public string? CardName { get; set; }
        public byte? DiscountPercentage { get; set; }
        public decimal? MoneyValue { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }

        public virtual CardType CardTypeKeyNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
