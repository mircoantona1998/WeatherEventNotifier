using System;
using System.Collections.Generic;

namespace SLAManager.Model;

public partial class SlaMetricViolationForecast
{
    public int Id { get; set; }

    public int? IdSla { get; set; }

    public string? Violation { get; set; }

    public DateTime? Datetime { get; set; }
}
