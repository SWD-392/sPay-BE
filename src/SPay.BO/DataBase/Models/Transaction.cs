﻿using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class Transaction
    {
        public string TransactionKey { get; set; } = null!;
        public string OrderKey { get; set; } = null!;
        public string? WithdrawKey { get; set; }
        public byte Status { get; set; }
        public DateTime InsDate { get; set; }

        public virtual Order OrderKeyNavigation { get; set; } = null!;
        public virtual Order? WithdrawKeyNavigation { get; set; }
    }
}
