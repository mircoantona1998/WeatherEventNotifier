using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models
{
    public partial class Job
    {
        public int Id { get; set; }
        public string? Job1 { get; set; }
        public bool? IsActive { get; set; }
        public int? HourToStart { get; set; }
        public int? MinuteToStart { get; set; }
        public DateTime? LastTimestampStart { get; set; }
        public DateTime? LastTimestampEnd { get; set; }
        public bool? Errors { get; set; }
    }
}
