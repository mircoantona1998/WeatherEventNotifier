﻿using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class SlaMetricStatus
{
    public int Id { get; set; }

    public int? IdSla { get; set; }

    public int? IdStatus { get; set; }

    public string? Action { get; set; }

    public string? Code { get; set; }

    public string? Controller { get; set; }

    public string? Endpoint { get; set; }

    public string? Instance { get; set; }

    public string? Job { get; set; }

    public string? Method { get; set; }

    public DateTime? Datetime { get; set; }

    public double? MisuredValue { get; set; }

    public double? DesiredValue { get; set; }
}
