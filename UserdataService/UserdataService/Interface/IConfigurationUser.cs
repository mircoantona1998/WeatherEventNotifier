namespace UserdataService.Interface
{
    public interface IConfigurationUser
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdFrequencyType { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime DateTimeCreate { get; set; }
        public DateTime DateTimeUpdate { get; set; }
        public DateTime DateActivation { get; set; }
        public bool IsActive { get; set; }
        public int IdMetric { get; set; }
    }
}
