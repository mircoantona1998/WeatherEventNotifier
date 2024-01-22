using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class SlaMetricStatusView
{
    public int IdSla { get; set; }

    public string? Symbol { get; set; }

    public double? Value { get; set; }

    public string? Metric { get; set; }

    public string? MetricDescription { get; set; }

    public string? Code { get; set; }

    public string? StatusDescription { get; set; }

    public DateTime? Datetime { get; set; }
}
