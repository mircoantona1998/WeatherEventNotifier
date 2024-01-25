

namespace SLAManagerdata.ViewModels
{
    #region Sla
    public class SlaAddDTO
    {
        public int IdMonitoringMetric { get; set; }
        public string Symbol { get; set; }
        public float DesiredValue { get; set; }
   
    }

    
    public class SlaPatchDTO
    {
        public int Id { get; set; }
        public int IdMonitoringMetric { get; set; }
        public string Symbol { get; set; }
        public float DesiredValue { get; set; }

    }

    
    #endregion
}