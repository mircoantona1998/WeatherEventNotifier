using System;
using System.Collections.Generic;

namespace Userdata.Models;

public partial class TelegramMessage
{
    public int Id { get; set; }

    public string? IdChat { get; set; }

    public string? Testo { get; set; }

    public bool? Allegati { get; set; }

    public DateTime? DateCreate { get; set; }

    public DateTime? DateUpdate { get; set; }

    public bool? WasSent { get; set; }

    public DateTime? DateSent { get; set; }

    public bool IsActive { get; set; }

    public string? Result { get; set; }
}
