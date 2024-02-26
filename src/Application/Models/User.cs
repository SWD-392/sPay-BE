using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? Role { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Admin> Admins { get; } = new List<Admin>();

    public virtual Customer? Customer { get; set; }

    public virtual StoreOwner? StoreOwner { get; set; }

    public virtual ICollection<TopupMember> TopupMembers { get; } = new List<TopupMember>();
}
