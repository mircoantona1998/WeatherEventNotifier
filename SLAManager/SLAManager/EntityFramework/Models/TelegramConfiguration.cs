using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class TelegramConfiguration
{
    public int Id { get; set; }

    public string? Bot { get; set; }

    public string? Token { get; set; }
}
