namespace SLAManager.Interface
{
    public interface IFrequency
    {
        public int? Id { get; set; }
        public string? FrequencyName { get; set; }
        public int? Minutes { get; set; }
        public bool? IsActive { get; set; }
    }
}
