using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class Admin
{
    public int AdminKey { get; set; }

    public int UserKey { get; set; }

    public string? AdminName { get; set; }

    public string? CreateAt { get; set; }

    public virtual User UserKeyNavigation { get; set; } = null!;
}
