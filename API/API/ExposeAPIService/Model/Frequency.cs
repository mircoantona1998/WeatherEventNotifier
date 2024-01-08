using Confluent.Kafka;
using ExposeAPI.Interface;
using ExposeAPI.Kafka;

namespace ExposeAPI.Model
{
    public class Frequency : IFrequency
    {
        public int? Id { get; set; }
        public string? FrequencyName { get; set; }
        public int? Minutes { get; set; }
        public bool? IsActive { get; set; }

    }

}
