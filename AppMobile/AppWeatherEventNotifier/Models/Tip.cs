using System.Collections.Generic;

namespace AppWeatherEventNotifier.Models
{
   public class Tip
    {
        public int Id { get; set; }

        public int? IdUser { get; set; }

        public int? IdSchedule { get; set; }

        public string Message { get; set; }

        public DateTime? DateTimeCreate { get; set; }

        public int? IdConfiguration { get; set; }

        public float? ValueWeather { get; set; }
    }
}
