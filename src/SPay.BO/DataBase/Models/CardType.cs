using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class CardType
    {
        public CardType()
        {
            Cards = new HashSet<Card>();
        }

        public string CardTypeKey { get; set; } = null!;
        public string CardTypeName { get; set; } = null!;
        public string? Description { get; set; }
        public byte Status { get; set; }
        public DateTime InsDate { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
