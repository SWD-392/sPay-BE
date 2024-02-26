using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class TopupMember
{
    public int TopupId { get; set; }

    public int? UserId { get; set; }

    public int? Amount { get; set; }

    public DateOnly? Date { get; set; }

    public bool? Status { get; set; }

    public virtual User? User { get; set; }
}
