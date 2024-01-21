using AutoMapper;
using SLAManagerdata.ViewModels;
using Microsoft.EntityFrameworkCore;
using static EntityFramework.Classes.SLAManagerdataLib;



namespace SLAManagerdata.Models
{
    public class MonitoringMetricRepository
    {
        private readonly MapperConfiguration mapperConfig;
        private readonly DbContextOptionsBuilder<SlamanagerContext> DB;

        public MonitoringMetricRepository(string config)
        {
            DB = new DbContextOptionsBuilder<SlamanagerContext>().UseSqlServer(config);

        }
        #region GET
        /// <summary>
        /// Get MonitoringMetric
        public async Task<List<MonitoringMetric>> Get()
        {
            List<MonitoringMetric> mess = null;
            try
            {
                using var context = new SlamanagerContext(DB.Options);

                mess = await context.MonitoringMetrics
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
            }

            return mess;
        }
        #endregion
  

    }
}