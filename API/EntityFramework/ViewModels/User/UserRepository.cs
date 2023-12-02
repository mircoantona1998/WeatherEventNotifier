using AutoMapper;
using Userdata.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Userdata.Models
{
    public class UserRepository
    {
        public ILogger<UserRepository> _logger;
        private readonly MapperConfiguration mapperConfig;
        private readonly DbContextOptionsBuilder<UserdataContext> DB;

        public UserRepository(ILogger<UserRepository> logger, IConfiguration config)
        {
            _logger = logger;
            DB = new DbContextOptionsBuilder<UserdataContext>().UseSqlServer(config.GetConnectionString("Userdata"));
            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();                   
            });
        }
        #region GET
        /// <summary>
        /// Get users 
        /// </summary>
        public async Task<List<UserDTO>?> Get()
        {
            List<UserDTO>? users = null;
            try
            {
                using var context = new UserdataContext(DB.Options);
                var mapper = mapperConfig.CreateMapper();

                users = await context.Users
                    .AsNoTracking()                  
                    .OrderBy(user => user.Username)
                    .Select(user => mapper.Map(user, new UserDTO
                    {
                       
                    }))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting users: {ex.Message}");
            }

            return users;
        }
        #endregion
    }
}