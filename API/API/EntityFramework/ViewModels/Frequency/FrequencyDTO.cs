

namespace Userdata.ViewModels
{
    #region FREQUENCY
    public class FrequencyCreateDTO
    {
        public string FrequencyName { get; set; }
        public int Minutes { get; set; }
        public bool? IsActive { get; set; }
    }
    public class FrequencyPatchDTO
    {
        public int IdFrequency { get; set; }
        public string? FrequencyName { get; set; }
        public int? Minutes { get; set; }
        public bool? IsActive { get; set; }
    }
    #endregion
}