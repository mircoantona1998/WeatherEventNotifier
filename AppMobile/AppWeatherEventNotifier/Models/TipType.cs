using System.Collections.Generic;

namespace AppWeatherEventNotifier.Models
{
   public class TipType
    {
        public float? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string  ImagePath { get; set; }       
        public string CodeType { get; set; }
        public float? LimitUp { get; set; }
        public float? LimitDown { get; set; }
        public int? IterationDay { get; set; }
        public int? IterationHour { get; set; }
        public int? IterationMin { get; set; }
        public string TipMessage { get; set; }
        public string TipDescription { get; set; }
       
    }
}
