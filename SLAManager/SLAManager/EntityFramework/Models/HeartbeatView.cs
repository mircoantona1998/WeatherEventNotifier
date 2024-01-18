using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class HeartbeatView
{
    public string? Servicename { get; set; }

    public DateTime? Timestamp { get; set; }
}
