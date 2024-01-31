using AutoMapper;
using Userdata.ViewModels;
using Microsoft.EntityFrameworkCore;
using static EntityFramework.Utils.UserdataLib;
using System.Diagnostics;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Identity;
using Userdata.Utils;

namespace Userdata.Models
{
    public class UserRepository
    {
        private readonly MapperConfiguration mapperConfig;
        private readonly DbContextOptionsBuilder<UserdataContext> DB;

        public UserRepository( string config)
        {

           DB = new DbContextOptionsBuilder<UserdataContext>().UseSqlServer(config);
            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserInsertDTO, User>();
            });
        }
        public async Task<(int Cluster, int Partition)> GetMinClusterPartition(int maxCluster, int maxPartition)
        {
            try
            {
                List<(int, int)> clustersPartitions = new List<(int, int)>();

                for (int cluster = 0; cluster < maxCluster; cluster++)
                {
                    for (int partition = 0; partition < maxPartition; partition++)
                    {
                        clustersPartitions.Add((cluster, partition));
                    }
                }
                using var context = new UserdataContext(DB.Options);
                var counts = await context.Users
                    .Where(u => u.Cluster >= 0 && u.Cluster < maxCluster && u.Partition >= 0 && u.Partition < maxPartition)
                    .GroupBy(u => new { u.Cluster, u.Partition })
                    .Select(g => new { g.Key.Cluster, g.Key.Partition, Count = g.Count() })
                    .ToListAsync()
                    .ConfigureAwait(false);
                var unusedCombinations = new List<(int, int)>();
                foreach (var combination in clustersPartitions)
                {
                    var count = counts.FirstOrDefault(c => c.Cluster == combination.Item1 && c.Partition == combination.Item2);
                    if (count == null || count.Count == 0)
                    {
                        unusedCombinations.Add(combination);
                    }
                }
                if (unusedCombinations.Any())
                {
                    return unusedCombinations.First();
                }
                var minCount = counts.OrderBy(c => c.Count).First();
                return ((int)minCount.Cluster, (int)minCount.Partition);
            }
            catch (Exception ex)
            {
                Logger log = new Logger(); // Assicurati di definire una classe Logger appropriata
                log.LogAction($"An error occurred: {ex.Message}");
                throw;
            }
        }



        public async Task<int> GetTotalUsersCount()
        {
            try
            {
                using var context = new UserdataContext(DB.Options);
                int totalCount = await context.Users
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
                using var context = new UserdataContext(DB.Options);

                // Check if there are any users before attempting to find the maximum partition value
                bool anyUsers = await context.Users.AnyAsync().ConfigureAwait(false);

                if (!anyUsers)
                {
                    return 0;
                }

                int maxPartitionValue = await context.Users
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
        public async Task<bool?> Create(UserCreateDTO newItemDTO,int partition,int cluster)
        {
            bool? isCreated = false;
            try
            {
                UserInsertDTO insertDTO = new UserInsertDTO
                {
                    Username = newItemDTO.Username,
                    Password = newItemDTO.Password,
                    Address = newItemDTO.Address,
                    Cap = newItemDTO.Cap,
                    City = newItemDTO.City,
                    Cognome = newItemDTO.Cognome,
                    Nome = newItemDTO.Nome,
                    Partition = partition,
                    Cluster=cluster
                };
                using var context = new UserdataContext(DB.Options);
                var mapper = mapperConfig.CreateMapper();
                var newItem = mapper.Map(insertDTO, new User
                {
                    DateUpdate = DateTime.UtcNow,
                    IsActive = true,
                });
                bool userExists = await context.Users
                    .AsNoTracking()
                    .AnyAsync(user => user.Username == newItem.Username)
                    .ConfigureAwait(false);
                if (!userExists)
                {
                    using var transaction = await context.Database.BeginTransactionAsync().ConfigureAwait(false);
                    try
                    {
                        await context.Users.AddAsync(newItem);
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

        public async Task<(IdentityUser,int?,int?)> Login(LoginDTO loginDTO)
        {
            IdentityUser usr =new IdentityUser();
            try
            {
                using var context = new UserdataContext(DB.Options);
                var mapper = mapperConfig.CreateMapper();
                var usr1 = await context.Users
                    .Where(user => user.Username == loginDTO.Username && user.Password == loginDTO.Password)
                    .SingleOrDefaultAsync();
                if(usr1!=null)
                {
                    usr = new IdentityUser
                    {
                        Id = usr1.Id.ToString(),
                        UserName = usr1.Username,
                    };
                    return (usr,usr1.Cluster,usr1.Partition);
                }
                return (null,null,null);
            }
            catch (Exception ex)
            {
            }
            return (null,null,null);
        }

        public async Task<IdentityUser> Get_user_Login(string Username)
        {
            IdentityUser usr = new IdentityUser();
            try
            {
                using var context = new UserdataContext(DB.Options);
                var mapper = mapperConfig.CreateMapper();
                var usr1 = await context.Users
                    .Where(user => user.Username == Username )
                    .SingleOrDefaultAsync();
                if (usr1 != null)
                {
                    usr = new IdentityUser
                    {
                        Id = usr1.Id.ToString(),
                        UserName = usr1.Username,
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

        #region PATCH
        public async Task DistributeClustersPartitions(int clusters,int numPartitions)
        {
            try
            {
                using var context = new UserdataContext(DB.Options);
                var existingUsers = await context.Users.ToListAsync().ConfigureAwait(false);
                int totalUsers = existingUsers.Count;             
                for (int i = 0; i < totalUsers; i++)
                {

                    (int, int) clusterpartition = await GetMinClusterPartition(clusters, numPartitions);
                    existingUsers[i].Partition = clusterpartition.Item2;
                    existingUsers[i].Cluster = clusterpartition.Item1;
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Logger log = new();
                log.LogAction($"An error occurred: {ex.Message}");
            }
        }
        #endregion
    }
}