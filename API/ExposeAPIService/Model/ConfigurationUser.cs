using Confluent.Kafka;
using ExposeAPI.Interface;
using ExposeAPI.Kafka;

namespace ExposeAPI.Model
{
    public class ConfigurationUser : IConfigurationUser
    {
        public int? Id { get ; set ; }
        public int? IdUser { get ; set ; }
        public int? IdFrequency { get ; set ; }
        public double? Longitude { get ; set ; }
        public double? Latitude { get ; set ; }
        public DateTime? DateTimeCreate { get ; set ; }
        public DateTime? DateTimeUpdate { get ; set ; }
        public DateTime? DateTimeActivation { get ; set ; }
        public bool? IsActive { get ; set ; }
        public int? IdMetric { get ; set ; }
    }

}
