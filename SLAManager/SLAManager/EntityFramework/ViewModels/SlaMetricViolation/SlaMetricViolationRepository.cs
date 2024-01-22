using AutoMapper;
using SLAManagerdata.ViewModels;
using Microsoft.EntityFrameworkCore;
using static EntityFramework.Classes.SLAManagerdataLib;



namespace SLAManagerdata.Models
{
    public class SlaMetricViolationRepository
    {
        private readonly MapperConfiguration mapperConfig;
        private readonly DbContextOptionsBuilder<SlamanagerContext> DB;

        public SlaMetricViolationRepository(string config)
        {
            DB = new DbContextOptionsBuilder<SlamanagerContext>().UseSqlServer(config);

        }
        #region GET
        /// <summary>
        /// Get SlaMetricViolation
        public async Task<List<SlaMetricViolationView>> Get()
        {
            List<SlaMetricViolationView> mess = null;
            try
            {
                using var context = new SlamanagerContext(DB.Options);

                mess = await context.SlaMetricViolationViews
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