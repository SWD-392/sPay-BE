using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class StoreOwner
{
    public int StoreOwnerId { get; set; }

    public int? UserId { get; set; }

    public string? OwnerName { get; set; }

    public int? StoreId { get; set; }

    public virtual Store? Store { get; set; }

    public virtual User? User { get; set; }
}
