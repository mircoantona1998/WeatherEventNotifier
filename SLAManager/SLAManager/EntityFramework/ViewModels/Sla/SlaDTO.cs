

namespace SLAManagerdata.ViewModels
{
    #region Sla
    public class SlaAddDTO
    {
        public int IdMonitoringMetric { get; set; }
        public float? FromDesiredValue { get; set; }
        public float? ToDesiredValue { get; set; }

    }

    
    public class SlaPatchDTO
    {
        public int Id { get; set; }
        public int IdMonitoringMetric { get; set; }
        public float? FromDesiredValue { get; set; }
        public float? ToDesiredValue { get; set; }

    }

    
    #endregion
}