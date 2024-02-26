using System;
using System.Collections.Generic;

namespace Application.Common.Database.Models;

public partial class TopupMember
{
    public int TopupMemberKey { get; set; }

    public int UserKey { get; set; }

    public int? Amount { get; set; }

    public DateOnly? Date { get; set; }

    public bool? Status { get; set; }
}
