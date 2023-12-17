using System.Collections.Generic;

namespace AppWeatherEventNotifier.Models
{
   public class Frequency
    {
        public int? Id { get; set; }
        public string? FrequencyName { get; set; }
        public int? Minutes { get; set; }
        public bool? IsActive { get; set; }

    }
}
