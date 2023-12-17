

using Microsoft.Extensions.Configuration;

namespace Userdata.ViewModels
{
    #region CONFIGURATION
    public class ConfigurationCreateDTO
    {
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public int IdMetric { get; set; }
        public int IdFrequency { get; set; }
        public DateTime? DateTimeActivation{ get; set; }
    }
    public class ConfigurationCreateRequestKafka : ConfigurationCreateDTO
    {
        public int IdUser { get; set; }
        public static ConfigurationCreateRequestKafka ConvertConfigurationCreateToRequestKafka(ConfigurationCreateDTO dto, int idUser)
        {
            return new ConfigurationCreateRequestKafka
            {
                Longitude = dto.Longitude,
                Latitude = dto.Latitude,
                IdMetric = dto.IdMetric,
                IdFrequency = dto.IdFrequency,
                DateTimeActivation = dto.DateTimeActivation,
                IdUser = idUser
            };
        }
    }
    
    public class ConfigurationPatchDTO
    {
        public int IdConfiguration { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public int? IdMetric { get; set; }
        public int? IdFrequency { get; set; }
        public DateTime? DateTimeActivation { get; set; }
    }
    public class ConfigurationPatchRequestKafka : ConfigurationPatchDTO
    {
        public int IdUser { get; set; }
        public static ConfigurationPatchRequestKafka ConvertConfigurationPatchToRequestKafka(ConfigurationPatchDTO dto, int idUser)
        {
            return new ConfigurationPatchRequestKafka
            {
                IdConfiguration = dto.IdConfiguration,
                Longitude = dto.Longitude,
                Latitude = dto.Latitude,
                IdMetric = dto.IdMetric,
                IdFrequency = dto.IdFrequency,
                DateTimeActivation = dto.DateTimeActivation,
                IdUser = idUser
            };
        }
    }
    
    #endregion
}