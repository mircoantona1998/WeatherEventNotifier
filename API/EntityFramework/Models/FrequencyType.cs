using System;
using System.Collections.Generic;

namespace Userdata.Models
{
    public partial class FrequencyType
    {
        public int Id { get; set; }
        public string? FrequencyName { get; set; }
        public int? Minutes { get; set; }
    }
}
