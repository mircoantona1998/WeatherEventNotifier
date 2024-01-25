using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class SlaMetricStatusView
{
    public int IdSla { get; set; }

    public double? FromDesiredValue { get; set; }

    public double? ToDesiredValue { get; set; }

    public double? MisuredValue { get; set; }

    public string? Metric { get; set; }

    public string? MetricDescription { get; set; }

    public string? StatusCode { get; set; }

    public string? StatusDescription { get; set; }

    public DateTime? Datetime { get; set; }

    public string? Action { get; set; }

    public string? Code { get; set; }

    public string? Controller { get; set; }

    public string? Endpoint { get; set; }

    public string? Instance { get; set; }

    public string? Job { get; set; }

    public string? Method { get; set; }
}
