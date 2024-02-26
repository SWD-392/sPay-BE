using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class StoreOwner
{
    public int StoreOwnerKey { get; set; }

    public int UserKey { get; set; }

    public string? OwnerName { get; set; }

    public int? StoreKey { get; set; }

    public string? CreateAt { get; set; }

    public virtual Store? StoreKeyNavigation { get; set; }

    public virtual User UserKeyNavigation { get; set; } = null!;
}
