using Confluent.Kafka;
using ExposeAPI.Interface;
using ExposeAPI.Kafka;

namespace ExposeAPI.Model
{
    public class Schedulation : ISchedulation
    {
        public int Id { get; set; }
        public int? IdConfiguration { get; set; }
        public DateTime? DateTimeToSchedule { get; set; }
        public string FieldMetric { get; set; }
        public string Symbol { get; set; }
        public float? Value { get; set; }
        public int? IdUser { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public string ParentMetric { get; set; }
        public string ValueUnit { get; set; }
        public string NameConfiguration { get; set; }

    }

}
