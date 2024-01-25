using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class SlaView
{
    public int Id { get; set; }

    public int? IdMonitoringMetric { get; set; }

    public double? FromDesiredValue { get; set; }

    public double? ToDesiredValue { get; set; }

    public DateTime? UpdateDatetime { get; set; }

    public string? Metric { get; set; }

    public string? Description { get; set; }
}
