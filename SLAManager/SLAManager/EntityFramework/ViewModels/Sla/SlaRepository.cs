using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SLAManagerdata.ViewModels;
using SLAManagerdata.Utils;
using EntityFramework.Utils;
using System.Linq;

namespace SLAManagerdata.Models
{
    public class SlaRepository
    {
        private readonly MapperConfiguration mapperConfig;
        private readonly DbContextOptionsBuilder<SlamanagerContext> DB;

        public SlaRepository( string config)
        {

           DB = new DbContextOptionsBuilder<SlamanagerContext>().UseSqlServer(config);
            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SlaAddDTO, Sla>();
                cfg.CreateMap<SlaPatchDTO, Sla>();
            });
        }


        #region POST
        public async Task<bool?> Create(SlaAddDTO newItemDTO)//,int partition)
        {
            bool? isCreated = false;
            using var context = new SlamanagerContext(DB.Options);
            var mapper = mapperConfig.CreateMapper();
            using var transaction = await context.Database.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                SlaAddDTO insertDTO = new SlaAddDTO
                {
                    IdMonitoringMetric= newItemDTO.IdMonitoringMetric,
                    Symbol =newItemDTO.Symbol,
                    DesiredValue = newItemDTO.DesiredValue,
                   // Partition=partition
                };

                var newItem = mapper.Map(insertDTO, new Sla
                {
                    UpdateDatetime = DateTime.UtcNow,
                });
                bool slasExists = await context.Slas
                    .AsNoTracking()
                    .AnyAsync(sl => sl.IdMonitoringMetric== newItemDTO.IdMonitoringMetric)
                    .ConfigureAwait(false);
                bool metricExists = await context.MonitoringMetrics
                    .AsNoTracking()
                    .AnyAsync(sl => sl.Id == newItemDTO.IdMonitoringMetric)
                    .ConfigureAwait(false);

                if (!slasExists && metricExists==true)
                {
                    await context.Slas.AddAsync(newItem);
                    isCreated = !UserdataLib.IsNullOrZero(await context.SaveChangesAsync().ConfigureAwait(false));
                    await transaction.CommitAsync().ConfigureAwait(false);
                }            
            }
            catch (Exception ex)
            {
                Logger log = new();
                log.LogAction($"An error occurred: {ex.Message}");
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw; 
            }

            return isCreated;
        }
        #endregion


        #region GET
        public async Task<List<SlaView>> Get()
        {
            List<SlaView> list= new List<SlaView>();
            try
            {
                using var context = new SlamanagerContext(DB.Options);
                list = await context.SlaViews
                    .AsNoTracking()
                    .ToListAsync();
               
            }
            catch (Exception ex)
            {
            }
            return list;
        }
        #endregion

        #region PATCH
        public async Task<bool?> Patch(SlaPatchDTO newItemDTO)//, int partition)
        {
            bool? isPatched = false;
            using var context = new SlamanagerContext(DB.Options);
            var mapper = mapperConfig.CreateMapper();
            using var transaction = await context.Database.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                SlaPatchDTO insertDTO = new SlaPatchDTO
                {
                    Id = newItemDTO.Id,
                    IdMonitoringMetric = newItemDTO.IdMonitoringMetric,
                    Symbol = newItemDTO.Symbol,
                    DesiredValue = newItemDTO.DesiredValue,
                   // Partition=partition
                };

                var newItem = mapper.Map(insertDTO, new Sla
                {
                    UpdateDatetime = DateTime.UtcNow,
                });
                bool slasExists = await context.Slas
                    .AsNoTracking()
                    .AnyAsync(sl => sl.IdMonitoringMetric == newItemDTO.IdMonitoringMetric && sl.Id != newItemDTO.Id)
                    .ConfigureAwait(false);
                bool metricExists = await context.MonitoringMetrics
                    .AsNoTracking()
                    .AnyAsync(sl => sl.Id == newItemDTO.IdMonitoringMetric)
                    .ConfigureAwait(false);

                if (!slasExists && metricExists == true)
                {
                    var sla=await context.Slas
                    .AsNoTracking()
                    .Where(sl => sl.Id== newItemDTO.Id).FirstOrDefaultAsync();
                    if(sla!=null) { 
                        sla.DesiredValue = newItemDTO.DesiredValue;
                        sla.IdMonitoringMetric= newItemDTO.IdMonitoringMetric;
                        sla.Symbol=newItemDTO.Symbol;
                        sla.UpdateDatetime= DateTime.UtcNow;
                        context.Entry(sla).State = EntityState.Modified;
                        await context.SaveChangesAsync();
                        await transaction.CommitAsync().ConfigureAwait(false);
                        isPatched = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger log = new();
                log.LogAction($"An error occurred: {ex.Message}");
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }

            return isPatched;
        }
        #endregion

        #region DELETE
        public async Task<bool> Delete(int? idSla)
        {
            try
            {
                using var context = new SlamanagerContext(DB.Options);
                var recordsToDelete = await context.Slas
                 .Where(rec => rec.Id==idSla)
                 .ToListAsync();
                context.Slas.RemoveRange(recordsToDelete);
               int deleteN= await context.SaveChangesAsync();
                if (deleteN > 0) return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
        #endregion
    }
}