using System;
using System.Collections.Generic;

namespace Application.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public int? UserId { get; set; }

    public string? AdminName { get; set; }

    public virtual User? User { get; set; }
}
