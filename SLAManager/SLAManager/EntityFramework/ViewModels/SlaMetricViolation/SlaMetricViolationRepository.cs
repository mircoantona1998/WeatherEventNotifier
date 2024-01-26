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
        public async Task<List<SlaMetricViolation>> Get(int? hours)
        {
            List<SlaMetricViolation> mess = null;
            try
            {
                using var context = new SlamanagerContext(DB.Options);
                if (hours == null) { 
                mess = await context.SlaMetricViolations
                    .AsNoTracking()
                    .OrderByDescending(mes => mes.Id)
                    .Take(100)
                    .ToListAsync();
                }else
                {
                    DateTime thresholdDateTime = DateTime.UtcNow.AddHours(-hours.Value);

                    mess = await context.SlaMetricViolations
                                   .Where(x => x.Datetime > thresholdDateTime)
                                   .OrderByDescending(mes => mes.Id)
                                   .AsNoTracking()
                                   .Take(100)
                                   .ToListAsync();
                }
            }
            catch (Exception ex)
            {
            }

            return mess;
        }
        public async Task<int> GetCount(int? hours)
        {
            List<SlaMetricViolation> mess = null;
            try
            {
                using var context = new SlamanagerContext(DB.Options);
                if (hours == null)
                {
                    mess = await context.SlaMetricViolations
                        .AsNoTracking()
                        .ToListAsync();
                }
                else
                {
                    DateTime thresholdDateTime = DateTime.UtcNow.AddHours(-hours.Value);

                    mess = await context.SlaMetricViolations
                                   .Where(x => x.Datetime > thresholdDateTime)
                                   .AsNoTracking()
                                   .ToListAsync();
                }
            }
            catch (Exception ex)
            {
            }

            return mess.Count;
        }
        #endregion


    }
}