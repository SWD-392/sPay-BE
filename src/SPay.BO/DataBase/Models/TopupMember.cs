using System;
using System.Collections.Generic;

namespace SPay.BO.DataBase.Models
{
    public partial class TopupMember
    {
        public string TopupMemberKey { get; set; } = null!;
        public string UserKey { get; set; } = null!;
        public int? Amount { get; set; }
        public DateTime? Date { get; set; }
        public bool? Status { get; set; }
    }
}
