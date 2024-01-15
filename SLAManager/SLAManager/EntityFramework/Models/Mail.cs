using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class Mail
{
    public int Id { get; set; }

    public string? Mittente { get; set; }

    public string? Destinatario { get; set; }

    public string? Oggetto { get; set; }

    public string? Testo { get; set; }

    public bool? Allegati { get; set; }

    public DateTime? DateCreate { get; set; }

    public DateTime? DateUpdate { get; set; }

    public bool? WasSent { get; set; }

    public DateTime? DateSent { get; set; }

    public bool IsActive { get; set; }

    public string? Result { get; set; }
}
