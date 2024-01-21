﻿

namespace SLAManagerdata.ViewModels
{
    #region Sla
    public class SlaAddDTO
    {

        public int? IdMonitoringMetric { get; set; }
        public int? Partition { get; set; }

        public string? Symbol { get; set; }

        public float? Value { get; set; }

    }

    
    public class SlaPatchDTO
    {
        public int Id { get; set; }

        public int? IdMonitoringMetric { get; set; }

        public string? Symbol { get; set; }

        public float? Value { get; set; }

    }

    
    #endregion
}