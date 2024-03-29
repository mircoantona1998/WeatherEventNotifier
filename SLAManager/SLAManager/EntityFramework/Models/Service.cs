﻿using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class Service
{
    public int Id { get; set; }

    public string? Service1 { get; set; }

    public string? Servicename { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? DateUpdate { get; set; }

    public int? IsBlocked { get; set; }

    public int? Partition { get; set; }

    public virtual ICollection<Heartbeat> Heartbeats { get; set; } = new List<Heartbeat>();
}
