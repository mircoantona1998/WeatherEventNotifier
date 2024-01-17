using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class Heartbeat
{
    public int Id { get; set; }

    public int? IdService { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual Service? IdServiceNavigation { get; set; }
}
