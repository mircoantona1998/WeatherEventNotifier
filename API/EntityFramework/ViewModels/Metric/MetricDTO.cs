

namespace Userdata.ViewModels
{
    #region METRIC
    public class MetricCreateDTO
    {
        public string Field { get; set; }
        public string Type { get; set; }
        public string ValueUnit { get; set; }
        public bool? IsActive { get; set; }
    }
    public class MetricPatchDTO
    {
        public int IdMetric { get; set; }
        public string? Field { get; set; }
        public string? Type { get; set; }
        public string? ValueUnit { get; set; }
        public bool? IsActive { get; set; }
    }
    #endregion
}