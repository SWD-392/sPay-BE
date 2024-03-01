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
            DepositPackageCardTypes = new HashSet<DepositPackageCardType>();
        }

        public string CardTypeKey { get; set; } = null!;
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<CardStoreCategory> CardStoreCategories { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<DepositPackageCardType> DepositPackageCardTypes { get; set; }
    }
}
