﻿using AutoMapper;
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
        public async Task<List<MetricDatum>> Get(int? hours)
        {
            List<MetricDatum> mess = null;
            try
            {
                using var context = new SlamanagerContext(DB.Options);
                if (hours == null)
                {
                    mess = await context.MetricData
                        .AsNoTracking()
                        .OrderByDescending(mes => mes.Id)
                        .Take(100)
                        .ToListAsync();
                }
                else
                {
                    DateTime thresholdDateTime = DateTime.UtcNow.AddHours(-hours.Value);
                    mess = await context.MetricData
                        .AsNoTracking()
                        .Where(x => x.Timestamp > thresholdDateTime)
                        .OrderByDescending(mes => mes.Id)
                        .Take(100)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
            }

            return mess;
        }
        #endregion
  

    }
}