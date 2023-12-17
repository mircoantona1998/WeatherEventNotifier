using AutoMapper;
using Userdata.ViewModels;
using Microsoft.EntityFrameworkCore;
using static EntityFramework.Classes.UserdataLib;
using System.Diagnostics;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Identity;

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
                    DateUpdate = DateTime.UtcNow,
                    IsActive = true,
                });
                var usr = await context.Users
                    .AsNoTracking()
                    .Where(user =>user.Username==newItem.Username) 
                    .ToListAsync();
                if (usr.Any()==false)
                {     
                    await context.Users.AddAsync(newItem);
                    isCreated = !IsNullOrZero(await context.SaveChangesAsync()); // && !IsNullOrZero(usr);
                }

            }
            catch (Exception ex)
            {
            }

            return isCreated;
        }
        #endregion

        #region PATCH
        public async Task<IdentityUser> Login(LoginDTO loginDTO)
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
                    //// login = true;
                    // context.Attach(usr);
                    // //usr.LastAccess=DateTime.UtcNow;
                    // context.Update(usr);
                    // bool res = !IsNullOrZero(await context.SaveChangesAsync());
                    // if (res)
                    // {
                    // }
                    return usr;
                }
                return null;
            }
            catch (Exception ex)
            {
            }
            return null;
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

    }
}