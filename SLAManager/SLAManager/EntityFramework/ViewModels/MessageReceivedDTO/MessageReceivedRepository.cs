using AutoMapper;
using SLAManagerdata.ViewModels;
using Microsoft.EntityFrameworkCore;
using static EntityFramework.Classes.SLAManagerdataLib;



namespace SLAManagerdata.Models
{
    public class MessageReceivedRepository
    {
        private readonly MapperConfiguration mapperConfig;
        private readonly DbContextOptionsBuilder<SlamanagerContext> DB;

        public MessageReceivedRepository(string config)
        {
            DB = new DbContextOptionsBuilder<SlamanagerContext>().UseSqlServer(config);
            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MessageReceivedDTO, MessageReceived>();
                cfg.CreateMap<MessageReceived, MessageReceivedDTO>();
            });
        }
        #region GET
        /// <summary>
        /// Get last message
        public async Task<MessageReceivedDTO?> GetLast()
        {
            MessageReceivedDTO? mess = null;
            try
            {
                using var context = new SlamanagerContext(DB.Options);
                var mapper = mapperConfig.CreateMapper();

                mess = await context.MessageReceiveds
                    .AsNoTracking()
                    .OrderByDescending(mes => mes.Id)
                    .Select(mes=> mapper.Map(mes, new MessageReceivedDTO()))
                    .FirstAsync();
            }
            catch (Exception ex)
            {
            }

            return mess;
        }
        #endregion
        #region POST
        public async Task<bool?> Create(MessageReceivedDTO newItemDTO)
        {
            bool? isCreated = false;

            try
            {
                using var context = new SlamanagerContext(DB.Options);
                var mapper = mapperConfig.CreateMapper();

                var newItem = mapper.Map(newItemDTO, new MessageReceived
                {
                    Timestamp = DateTime.UtcNow,
                });
              
                await context.MessageReceiveds.AddAsync(newItem);
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