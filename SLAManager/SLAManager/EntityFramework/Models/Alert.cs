using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class Alert
{
    public int IdAlert { get; set; }

    public DateTime? Date { get; set; }

    public string? Note { get; set; }

    public bool? IsVisited { get; set; }

    public DateTime? DateVisited { get; set; }

    public int? IdUserVisited { get; set; }

    public bool? NeedInvestigation { get; set; }

    public DateTime? DateUpdate { get; set; }

    public int? Priority { get; set; }

    public bool? IsInvestigated { get; set; }

    public string? CodeAlert { get; set; }

    public string? Title { get; set; }

    public bool? IsResolved { get; set; }

    public string? CodeRecog { get; set; }

    public string? Type { get; set; }

    public int? IdCe { get; set; }

    public int? IdPod { get; set; }

    public virtual AlertCode? CodeAlertNavigation { get; set; }
}
