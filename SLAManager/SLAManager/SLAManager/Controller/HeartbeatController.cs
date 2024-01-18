using Microsoft.AspNetCore.Mvc;
using SLAManagerdata.Models;
using SLAManagerdata.ViewModels;
using SLAManager.Utils;
using SLAManager.Auth;
namespace SLAManager.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class HeartbeatController : ControllerBase
    {
        private readonly ServiceRepository serviceRepo;
        private readonly HeartBeatRepository hearthbeatRepo;
        private readonly IConfiguration _configuration;
        public HeartbeatController(IConfiguration configuration)
        {
            this._configuration = configuration;
            serviceRepo = new ServiceRepository(Environment.GetEnvironmentVariable("ConnectionStrings") ?? configuration.GetConnectionString("SLAManagerdata"));
            hearthbeatRepo = new HeartBeatRepository(Environment.GetEnvironmentVariable("ConnectionStrings") ?? configuration.GetConnectionString("Userdata"));
        }
        [HttpPost]
        [Route("Heartbeat")]
        public async Task<ActionResult> Send(LoginServiceDTO loginDTO)
        {
            Logger log = new();
            log.LogAction("HeartbeatController");
            AuthenticationResponse authResponse = null;
            var res = await serviceRepo.Login(loginDTO);
            if (res.Item1 != null)
            {
                authResponse = await AuthResponse.GenerateAuthResponse(res.Item1, _configuration, res.Item2);
                await hearthbeatRepo.Add(Convert.ToInt32(res.Item1.Id));
            }
            return authResponse != null ? Ok(authResponse) : Unauthorized();
        }

    }
    
}
