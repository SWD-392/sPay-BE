using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Card
    {
        public Card()
        {
            Deposits = new HashSet<Deposit>();
            Orders = new HashSet<Order>();
            Wallets = new HashSet<Wallet>();
        }

        public string CardKey { get; set; } = null!;
        public string? CustomerKey { get; set; }
        public string CardTypeKey { get; set; } = null!;
        public string? CardNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? Status { get; set; }
        public int NumberDate { get; set; }

        public virtual CardType CardTypeKeyNavigation { get; set; } = null!;
        public virtual Customer? CustomerKeyNavigation { get; set; }
        public virtual ICollection<Deposit> Deposits { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
