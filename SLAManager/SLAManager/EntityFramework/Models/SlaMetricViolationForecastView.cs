using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class SlaMetricViolationForecastView
{
    public int IdSla { get; set; }

    public string? Symbol { get; set; }

    public double? Value { get; set; }

    public string? Violation { get; set; }

    public DateTime? Datetime { get; set; }

    public string? Metric { get; set; }

    public string? MetricDescription { get; set; }
}
