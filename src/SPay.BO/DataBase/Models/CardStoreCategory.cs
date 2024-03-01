using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class CardStoreCategory
    {
        public string CardStoreCategoryKey { get; set; } = null!;
        public string CardTypeKey { get; set; } = null!;
        public string StoreCategoryKey { get; set; } = null!;

        public virtual CardType CardTypeKeyNavigation { get; set; } = null!;
        public virtual StoreCategory StoreCategoryKeyNavigation { get; set; } = null!;
    }
}
