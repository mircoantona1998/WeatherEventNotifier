﻿namespace ExposeAPI.Interface
{
    public interface IConfigurationUser
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
        public string? ValueUnit { get; set; }
        public string? NameConfiguration { get; set; }
    }
}
