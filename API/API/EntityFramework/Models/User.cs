﻿using System;
using System.Collections.Generic;

namespace Userdata.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public string? Cognome { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? DateUpdate { get; set; }

    public string? Address { get; set; }

    public string? Cap { get; set; }

    public string? City { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime? LastAccess { get; set; }

    public int? IsBlocked { get; set; }
}
