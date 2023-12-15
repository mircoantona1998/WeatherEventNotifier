

namespace Userdata.ViewModels
{
    #region CONFIGURATION
    public class ConfigurationCreateDTO
    {
        public int IdUser { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string Metric { get; set; }
        public string Frequency { get; set; }
    }
    public class ConfigurationPatchDTO
    {
        public int IdConfiguration { get; set; }
        public int? IdUser { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public string? Metric { get; set; }
        public string? Frequency { get; set; }
    }
    #endregion
}