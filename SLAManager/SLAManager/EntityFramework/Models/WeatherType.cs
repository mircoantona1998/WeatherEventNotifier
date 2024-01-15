using System;
using System.Collections.Generic;

namespace SLAManagerdata.Models
{
    public partial class WeatherType
    {
        public int Id { get; set; }
        public string? Field { get; set; }
        public string? ValueUnit { get; set; }
        public string? Period { get; set; }
    }
}
