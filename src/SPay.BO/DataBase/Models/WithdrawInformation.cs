using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class WithdrawInformation
    {
        public string WithdrawKey { get; set; } = null!;
        public string UserKey { get; set; } = null!;
        public decimal? Value { get; set; }
        public byte Status { get; set; }
        public DateTime InsDate { get; set; }
    }
}
