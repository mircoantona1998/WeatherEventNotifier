using AutoMapper;
using SLAManagerdata.ViewModels;
using Microsoft.EntityFrameworkCore;
using static EntityFramework.Classes.SLAManagerdataLib;



namespace SLAManagerdata.Models
{
    public class MetricDataRepository
    {
        private readonly MapperConfiguration mapperConfig;
        private readonly DbContextOptionsBuilder<SlamanagerContext> DB;

        public MetricDataRepository(string config)
        {
            DB = new DbContextOptionsBuilder<SlamanagerContext>().UseSqlServer(config);

        }
        #region GET
        /// <summary>
        /// Get MetricData
        public async Task<List<MetricDatum>> Get()
        {
            List<MetricDatum> mess = null;
            try
            {
                using var context = new SlamanagerContext(DB.Options);

                mess = await context.MetricData
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