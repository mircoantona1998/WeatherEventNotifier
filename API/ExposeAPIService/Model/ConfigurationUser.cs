using Confluent.Kafka;
using ExposeAPI.Interface;
using ExposeAPI.Kafka;

namespace ExposeAPI.Model
{
    public class ConfigurationUser : IConfigurationUser
    {
        public int? Id { get; set; }
        public int? IdUser { get; set; }
        public int? IdFrequency { get; set; }
        public int? IdMetric { get; set; }
        public string? FrequencyName { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public DateTime? DateTimeCreate { get; set; }
        public DateTime? DateTimeUpdate { get; set; }
        public DateTime? DateTimeActivation { get; set; }
        public bool? IsActive { get; set; }
        public string? Field { get; set; }
        public string? Description { get; set; }
        public string? Parent { get; set; }
        public string? Symbol { get; set; }
        public float? Value { get; set; }
        public string? ValueUnit { get; set; }
    }
}
