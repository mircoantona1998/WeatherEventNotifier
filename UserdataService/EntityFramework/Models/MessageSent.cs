using System;
using System.Collections.Generic;

namespace Userdata.Models
{
    public partial class MessageSent
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public int? Offset { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? Type { get; set; }
        public int? IdOffsetResponse { get; set; }
        public string? TagMessage { get; set; }
        public string? Topic { get; set; }
    }
}
