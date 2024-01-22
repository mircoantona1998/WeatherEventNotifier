using AutoMapper;
using SLAManagerdata.ViewModels;
using Microsoft.EntityFrameworkCore;
using static EntityFramework.Classes.SLAManagerdataLib;



namespace SLAManagerdata.Models
{
    public class SlaMetricViolationForecastRepository
    {
        private readonly MapperConfiguration mapperConfig;
        private readonly DbContextOptionsBuilder<SlamanagerContext> DB;

        public SlaMetricViolationForecastRepository(string config)
        {
            DB = new DbContextOptionsBuilder<SlamanagerContext>().UseSqlServer(config);

        }
        #region GET
        /// <summary>
        /// Get SlaMetricViolationForecast
        public async Task<List<SlaMetricViolationForecastView>> Get()
        {
            List<SlaMetricViolationForecastView> mess = null;
            try
            {
                using var context = new SlamanagerContext(DB.Options);

                mess = await context.SlaMetricViolationForecastViews
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