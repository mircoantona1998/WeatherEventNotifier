using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Service { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? DateUpdate { get; set; }

    public int? IsBlocked { get; set; }

    public int? Partition { get; set; }
}
