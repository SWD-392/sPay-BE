using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class CardType
    {
        public CardType()
        {
            CardStoreCategories = new HashSet<CardStoreCategory>();
            Cards = new HashSet<Card>();
        }

        public string CardTypeKey { get; set; } = null!;
        public string? CardTypeName { get; set; }
        public string? TypeDescription { get; set; }
        public bool? WithdrawalAllowed { get; set; }

        public virtual ICollection<CardStoreCategory> CardStoreCategories { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
    }
}
