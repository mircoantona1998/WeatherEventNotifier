using AutoMapper;
using Userdata.ViewModels;
using Microsoft.EntityFrameworkCore;
using static EntityFramework.Utils.UserdataLib;
using System.Diagnostics;
using Microsoft.IdentityModel.Logging;

namespace Userdata.Models
{
    public class MessageSentRepository
    {
        public ILogger<MessageSentRepository> _logger;
        private readonly MapperConfiguration mapperConfig;
        private readonly DbContextOptionsBuilder<UserdataContext> DB;

        public MessageSentRepository(string config)
        {
            DB = new DbContextOptionsBuilder<UserdataContext>().UseSqlServer(config);
            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MessageSentDTO, MessageSent>();
            });
        }
        #region GET
        /// <summary>
        /// Get last message
        public async Task<MessageSentDTO?> GetLast()
        {
            MessageSentDTO? mess = null;
            try
            {
                using var context = new UserdataContext(DB.Options);
                var mapper = mapperConfig.CreateMapper();

                mess = await context.MessageSents
                    .AsNoTracking()
                    .OrderByDescending(mes => mes.Id)
                    .Select(mes => mapper.Map(mes, new MessageSentDTO()))
                    .FirstAsync();
            }
            catch (Exception ex)
            {
            }

            return mess;
        }
        #endregion
        #region POST
        public async Task<bool?> Create(MessageSentDTO newItemDTO)
        {
            bool? isCreated = false;
            try
            {
                using var context = new UserdataContext(DB.Options);
                var mapper = mapperConfig.CreateMapper();
                var newItem = mapper.Map(newItemDTO, new MessageSent
                {
                    Timestamp = DateTime.UtcNow,
                });
                await context.MessageSents.AddAsync(newItem);
                isCreated = !IsNullOrZero(await context.SaveChangesAsync()) && !IsNullOrZero(newItem.Id);               
            }
            catch (Exception ex)
            {
            }

            return isCreated;
        }
        #endregion

    }
}