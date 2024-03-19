﻿using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Card
    {
        public string CardKey { get; set; } = null!;
        public string CardTypeKey { get; set; } = null!;
        public DateTime CardName { get; set; }
        public string? Description { get; set; }
        public string PromotionPackageKey { get; set; } = null!;
        public byte Status { get; set; }
        public bool WithdrawAllowed { get; set; }
        public DateTime InsDate { get; set; }

        public virtual CardType CardTypeKeyNavigation { get; set; } = null!;
        public virtual PromotionPackage PromotionPackageKeyNavigation { get; set; } = null!;
    }
}
