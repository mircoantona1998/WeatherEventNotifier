using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class MailConfiguration
{
    public int Id { get; set; }

    public string? Mail { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }
}
