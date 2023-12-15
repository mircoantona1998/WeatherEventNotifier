using System;
using System.Collections.Generic;

namespace Userdata.Models;

public partial class AlertCode
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public string? Variable { get; set; }

    public string? Type { get; set; }

    public bool? IsEnable { get; set; }

    public int? MinValue { get; set; }

    public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();
}
