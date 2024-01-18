using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class SlaMetricStatus
{
    public int Id { get; set; }

    public byte? SlaMetricStatus1 { get; set; }

    public DateTime? Datetime { get; set; }
}
