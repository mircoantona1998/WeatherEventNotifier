using AutoMapper;
using SLAManagerdata.ViewModels;
using Microsoft.EntityFrameworkCore;
using static EntityFramework.Classes.SLAManagerdataLib;


namespace SLAManagerdata.Models
{
    public class HeartBeatRepository
    {
        private readonly MapperConfiguration mapperConfig;
        private readonly DbContextOptionsBuilder<SlamanagerContext> DB;

        public HeartBeatRepository(string config)
        {
            DB = new DbContextOptionsBuilder<SlamanagerContext>().UseSqlServer(config);

        }

        #region POST
        public async Task<bool?> Add(int idService)
        {
            bool? isCreated = false;
            try
            {
                using var context = new SlamanagerContext(DB.Options);
                var existingHeartbeat = await context.Heartbeats.FirstOrDefaultAsync(h => h.IdService == idService);
                if (existingHeartbeat == null)
                {
                    Heartbeat newItem = new Heartbeat
                    {
                        IdService = idService,
                        Timestamp = DateTime.UtcNow,
                    };
                    await context.Heartbeats.AddAsync(newItem);
                    isCreated = !IsNullOrZero(await context.SaveChangesAsync());
                }
                else
                {
                    existingHeartbeat.Timestamp = DateTime.UtcNow;
                    isCreated = !IsNullOrZero(await context.SaveChangesAsync());
                }
            }
            catch (Exception ex)
            {
            }

            return isCreated;
        }
        #endregion

    }
}