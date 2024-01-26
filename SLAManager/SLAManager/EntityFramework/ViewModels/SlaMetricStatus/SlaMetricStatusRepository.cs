using AutoMapper;
using SLAManagerdata.ViewModels;
using Microsoft.EntityFrameworkCore;
using static EntityFramework.Classes.SLAManagerdataLib;



namespace SLAManagerdata.Models
{
    public class SlaMetricStatusRepository
    {
        private readonly MapperConfiguration mapperConfig;
        private readonly DbContextOptionsBuilder<SlamanagerContext> DB;

        public SlaMetricStatusRepository(string config)
        {
            DB = new DbContextOptionsBuilder<SlamanagerContext>().UseSqlServer(config);

        }
        #region GET
        /// <summary>
        /// Get SlaMetricStatus
        public async Task<List<SlaMetricStatusView>> Get()
        {
            List<SlaMetricStatusView> mess = null;
            try
            {
                using var context = new SlamanagerContext(DB.Options);

                mess = await context.SlaMetricStatusViews
                    .AsNoTracking()
                    .OrderByDescending(mes => mes.IdSla)
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