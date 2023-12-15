using AutoMapper;
using Userdata.ViewModels;
using Microsoft.EntityFrameworkCore;
using static EntityFramework.Classes.UserdataLib;
using System.Diagnostics;
using Microsoft.IdentityModel.Logging;

namespace Userdata.Models
{
    public class UserRepository
    {
        private readonly MapperConfiguration mapperConfig;
        private readonly DbContextOptionsBuilder<UserdataContext> DB;

        public UserRepository( IConfiguration config)
        {

            DB = new DbContextOptionsBuilder<UserdataContext>().UseSqlServer(config.GetConnectionString("Userdata"));
            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserCreateDTO, User>();
            });
        }

        #region POST
        public async Task<bool?> Create(UserCreateDTO newItemDTO)
        {
            bool? isCreated = false;
            try
            {
                using var context = new UserdataContext(DB.Options);
                var mapper = mapperConfig.CreateMapper();
                var newItem = mapper.Map(newItemDTO, new User
                {
                    DateUpdate = DateTime.Now,
                    IsActive = true,
                });
                var usr = await context.Users
                    .AsNoTracking()
                    .Where(user =>user.Username==newItem.Username) 
                    .ToListAsync();
                if (usr.Any()==false)
                {     
                    await context.Users.AddAsync(newItem);
                    isCreated = !IsNullOrZero(await context.SaveChangesAsync()) && !IsNullOrZero(newItem.Id);
                }

            }
            catch (Exception ex)
            {
            }

            return isCreated;
        }
        #endregion

        #region PATCH
        public async Task<bool?> Login(LoginDTO loginDTO)
        {
            bool? login = false;
            try
            {
                using var context = new UserdataContext(DB.Options);
                var mapper = mapperConfig.CreateMapper();
                User? usr = await context.Users
                    .AsNoTracking()
                    .Where(user => user.Username == loginDTO.Username && user.Password == loginDTO.Password)
                    .SingleOrDefaultAsync();
                if(usr!=null)
                {
                    login = true;
                    context.Attach(usr);
                    usr.LastAccess=DateTime.Now;
                    context.Update(usr);
                    bool res = !IsNullOrZero(await context.SaveChangesAsync());
                    if (res)
                    {
                    }
                }         
            }
            catch (Exception ex)
            {
            }

            return login;
        }
        #endregion

    }
}