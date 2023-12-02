using System.Collections.Generic;

namespace AppWeatherEventNotifier.Models
{
   public class Tip
    {
        public float? Id { get; set; }    
        public int? Priority { get; set; }
        public int? IdUtente { get; set; }
        public int? IdPod { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime? NoteDateValidityStart { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? NoteDateValidityEnd { get; set; }
        public TipType Type { get; set; }
        public string icon { get; set; }
        public string value { get; set; }
        public Color BackGround { get; set; }
        public string Message_descr{ get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string CodeType { get; set; }
        public float? LimitUp { get; set; }
        public float? LimitDown { get; set; }
        public int? IterationDay { get; set; }
        public int? IterationHour { get; set; }
        public int? IterationMin { get; set; }
        public string TipMessage { get; set; }
        public string TipDescription { get; set; }
        public bool responseToDo { get; set; }
    }
}
