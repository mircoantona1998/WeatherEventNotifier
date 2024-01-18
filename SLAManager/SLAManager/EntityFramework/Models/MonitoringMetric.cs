using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class MonitoringMetric
{
    public int Id { get; set; }

    public string? Metric { get; set; }

    public string? Description { get; set; }
}
