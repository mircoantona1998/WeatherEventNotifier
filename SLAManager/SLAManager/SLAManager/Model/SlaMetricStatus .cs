using System;
using System.Collections.Generic;

namespace SLAManager.Model;

public partial class SlaMetricStatus
{
    public int Id { get; set; }

    public byte? SlaMetricStatus1 { get; set; }

    public DateTime? Datetime { get; set; }
}
