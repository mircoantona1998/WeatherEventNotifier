using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models;

public partial class TelegramUser
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public string ChatId { get; set; } = null!;
}
