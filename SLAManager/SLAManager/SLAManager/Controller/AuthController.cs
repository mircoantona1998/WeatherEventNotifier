using Microsoft.AspNetCore.Mvc;
using SLAManagerdata.Models;
using SLAManagerdata.ViewModels;
using SLAManager.Utils;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using SLAManager.Auth;
using Confluent.Kafka;
namespace SLAManager.Controllers
{
[Route("[controller]")]
[ApiController]

public class AuthController : ControllerBase
{
        private readonly ServiceRepository userRepo;
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            this._configuration = configuration;
            userRepo = new ServiceRepository(Environment.GetEnvironmentVariable("ConnectionStrings") ?? configuration.GetConnectionString("SLAManagerdata"));
        }

        #region POST
        [HttpPost]
        [Route("Registration")]
        public async Task<ActionResult> Create(ServiceCreateDTO newItemDTO)
        {
            Logger log = new();
            log.LogAction("AuthController  Create");
            //string valueEnv = Environment.GetEnvironmentVariable("HowManyPartition") ?? "2";
            //int partition=await userRepo.GetMinPartition(Convert.ToInt32(valueEnv));
            var newItemID = await userRepo.Create(newItemDTO);//,partition);
            return (bool)newItemID ? Ok(newItemID) : Problem(null, null, 401);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            Logger log = new();
            log.LogAction("AuthController  Login");
            AuthenticationResponse authResponse = null;
            var res = await userRepo.Login(loginDTO);
            if (res.Item1 !=null)
            {
                 authResponse = await AuthResponse.GenerateAuthResponse(res.Item1,_configuration, res.Item2);
            }
            return authResponse != null ? Ok(authResponse) : Unauthorized();
        }

        //[HttpPost]
        //[Route("refresh-token")]
        //public async Task<IActionResult> RefreshToken(TokenCustom jwtToken)
        //{
        //    Logger log = new();
        //    log.LogAction("AuthController  RefreshToken");
        //    AuthenticationResponse? authResponse = null;
        //    if (string.IsNullOrWhiteSpace(jwtToken.access_token) || string.IsNullOrWhiteSpace(jwtToken.refresh_token))
        //    {
        //        return BadRequest("Access and refresh tokens must be both non-nullable and non-empty");
        //    }
        //    var tokenModule = new TokenAuthenticationModule();
        //    var principal = tokenModule.GetPrincipalFromExpiredToken(jwtToken.access_token,_configuration);
        //    if (principal != null)
        //    {
        //        var username = principal.Identity?.Name;
        //        var refreshTokenSaved = tokenModule.GenerateRefreshToken(username);
        //        var loggingUser = await userRepo.Get_user_Login(username);

        //        if (loggingUser != null && refreshTokenSaved == jwtToken.refresh_token)
        //        {
        //            authResponse = await AuthResponse.GenerateAuthResponse(loggingUser, _configuration, true);
        //        }
        //    }
        //    return authResponse != null ? Ok(authResponse) : Unauthorized();
        //}
        #endregion
        
        }
    
}
