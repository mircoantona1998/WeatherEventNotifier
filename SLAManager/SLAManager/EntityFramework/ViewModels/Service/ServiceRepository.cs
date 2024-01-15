using AutoMapper;
using Userdata.ViewModels;
using Microsoft.EntityFrameworkCore;
using static EntityFramework.Classes.UserdataLib;
using System.Diagnostics;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Identity;
using Userdata.Utils;


namespace Userdata.Models
{
    public class ServiceRepository
    {
        private readonly MapperConfiguration mapperConfig;
        private readonly DbContextOptionsBuilder<SlamanagerContext> DB;

        public ServiceRepository( string config)
        {

           DB = new DbContextOptionsBuilder<SlamanagerContext>().UseSqlServer(config);
            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ServiceInsertDTO, Service>();
            });
        }
        public async Task<int> GetMinPartition(int maxPartition)
        {
            try
            {
                List<(int, bool)> partitions = [];
                int x = 0;
                while (x < maxPartition)
                {
                    partitions.Add((x, false));
                    x++;
                }

                using var context = new SlamanagerContext(DB.Options);
                var partitionCounts = await context.Services
                    .Where(u => u.Partition >= 0 && u.Partition <= maxPartition)
                    .GroupBy(u => u.Partition)
                    .Select(g => new { Partition = g.Key, Count = g.Count() })
                    .OrderBy(u=> u.Partition)
                    .ToListAsync()
                    .ConfigureAwait(false);


                x = 0;
                while(x < partitionCounts.Count)
                {
                    partitions[x] = (x, true);
                    x++;
                }

                x = 0;
                while (x < partitions.Count)
                {
                    if (partitions[x] == (x, false))
                        return x;
                    x++;
                }

                var minPartitionCount = partitionCounts
                    .OrderBy(g => g.Count)
                    .FirstOrDefault();

                int minPartition = minPartitionCount?.Partition ?? 0;

                return minPartition;
            }
            catch (Exception ex)
            {
                Logger log = new();
                log.LogAction($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<int> GetTotalUsersCount()
        {
            try
            {
                using var context = new SlamanagerContext(DB.Options);
                int totalCount = await context.Services
                    .AsNoTracking()
                    .CountAsync()
                    .ConfigureAwait(false);

                return totalCount;
            }
            catch (Exception ex)
            {
                Logger log = new();
                log.LogAction($"An error occurred: {ex.Message}");
                throw; 
            }
        }
        public async Task<int> GetMaxPartitionValue()
        {
            try
            {
                using var context = new SlamanagerContext(DB.Options);

                // Check if there are any users before attempting to find the maximum partition value
                bool anyUsers = await context.Services.AnyAsync().ConfigureAwait(false);

                if (!anyUsers)
                {
                    return 0;
                }

                int maxPartitionValue = await context.Services
                    .AsNoTracking()
                    .MaxAsync(user => (int?)user.Partition ?? 0)
                    .ConfigureAwait(false);

                return maxPartitionValue;
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.LogAction($"An error occurred: {ex.Message}");
                throw;
            }
        }

        #region POST
        public async Task<bool?> Create(ServiceCreateDTO newItemDTO)//,int partition)
        {
            bool? isCreated = false;
            try
            {
                ServiceInsertDTO insertDTO = new ServiceInsertDTO
                {
                    Service=newItemDTO.Service,
                    Servicename = newItemDTO.Servicename,
                    Password = newItemDTO.Password,
                    Partition = 0
                };
                using var context = new SlamanagerContext(DB.Options);
                var mapper = mapperConfig.CreateMapper();
                var newItem = mapper.Map(insertDTO, new Service
                {
                    DateUpdate = DateTime.UtcNow,
                    IsActive = true,
                });
                bool userExists = await context.Services
                    .AsNoTracking()
                    .AnyAsync(user => user.Servicename == newItem.Servicename)
                    .ConfigureAwait(false);
                if (!userExists)
                {
                    using var transaction = await context.Database.BeginTransactionAsync().ConfigureAwait(false);
                    try
                    {
                        await context.Services.AddAsync(newItem);
                        isCreated = !IsNullOrZero(await context.SaveChangesAsync().ConfigureAwait(false));
                        await transaction.CommitAsync().ConfigureAwait(false);
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync().ConfigureAwait(false);
                        throw; 
                    }
                }
            }
            catch (Exception ex)
            {
                Logger log = new();
                log.LogAction($"An error occurred: {ex.Message}");
            }

            return isCreated;
        }
        #endregion


        #region PATCH
        public async Task<(IdentityUser,int?)> Login(LoginDTO loginDTO)
        {
            IdentityUser usr =new IdentityUser();
            try
            {
                using var context = new SlamanagerContext(DB.Options);
                var mapper = mapperConfig.CreateMapper();
                var usr1 = await context.Services
                    .Where(user => user.Servicename == loginDTO.Servicename && user.Password == loginDTO.Password)
                    .SingleOrDefaultAsync();
                if(usr1!=null)
                {
                    usr = new IdentityUser
                    {
                        Id = usr1.Id.ToString(),
                        UserName = usr1.Servicename,
                    };
                    return (usr,usr1.Partition);
                }
                return (null,null);
            }
            catch (Exception ex)
            {
            }
            return (null,null);
        }

        public async Task<IdentityUser> Get_user_Login(string Username)
        {
            IdentityUser usr = new IdentityUser();
            try
            {
                using var context = new SlamanagerContext(DB.Options);
                var mapper = mapperConfig.CreateMapper();
                var usr1 = await context.Services
                    .Where(user => user.Servicename == Username )
                    .SingleOrDefaultAsync();
                if (usr1 != null)
                {
                    usr = new IdentityUser
                    {
                        Id = usr1.Id.ToString(),
                        UserName = usr1.Servicename,
                    };

                    return usr;
                }
                return null;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        #endregion

    }
}