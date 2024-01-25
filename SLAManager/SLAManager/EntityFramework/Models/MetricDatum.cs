using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class MetricDatum
{
    public int Id { get; set; }

    public string? MetricName { get; set; }

    public string? Action { get; set; }

    public string? Code { get; set; }

    public string? Controller { get; set; }

    public string? Endpoint { get; set; }

    public string? Instance { get; set; }

    public string? Job { get; set; }

    public string? Method { get; set; }

    public double? Value1 { get; set; }

    public string? Value2 { get; set; }

    public DateTime? Timestamp { get; set; }
}
