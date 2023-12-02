using System;
using System.Collections.Generic;

namespace Userdata.Models
{
    public partial class Logging
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? LongMessage { get; set; }
        public DateTime? DateCreate { get; set; }
        public string? File { get; set; }
        public string? Level { get; set; }
        public string? Module { get; set; }
        public string? RecoveryAction { get; set; }
    }
}
