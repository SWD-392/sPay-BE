using System;
using System.Collections.Generic;

namespace Application.Common.Database.Models;

public partial class User
{
    public int UserKey { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? Role { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Admin> Admins { get; } = new List<Admin>();

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<StoreOwner> StoreOwners { get; } = new List<StoreOwner>();
}
