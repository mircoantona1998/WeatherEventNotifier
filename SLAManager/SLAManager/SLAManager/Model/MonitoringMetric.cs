using System;
using System.Collections.Generic;

namespace SLAManager.Model;

public partial class MonitoringMetric
{
    public int Id { get; set; }

    public string? Metric { get; set; }

    public string? Description { get; set; }
}
