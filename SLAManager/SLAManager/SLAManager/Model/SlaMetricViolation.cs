using System;
using System.Collections.Generic;

namespace SLAManager.Model;

public partial class SlaMetricViolation
{
    public int Id { get; set; }

    public int? IdSla { get; set; }

    public string? Violation { get; set; }

    public DateTime? Datetime { get; set; }
}
