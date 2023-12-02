using Userdata.Models;
using Userdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly UserRepository userRepo;

        public UserController(ILogger<UserRepository> logger, IConfiguration config)
        {
            _logger = logger;
            userRepo = new UserRepository(_logger, config);
        }

        #region GET
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult> Get(bool? current)
        {
            //LogRequestStart(_logger, $"{GetType().FullName}/{System.Reflection.MethodBase.GetCurrentMethod().Name}");
            //get user identity
            //get data if authorized
            var users = await userRepo.Get();

            return users != null ? Ok(current == true ? users.SingleOrDefault() : users) : Problem(null, null, 401);
        }
        #endregion
    }
}