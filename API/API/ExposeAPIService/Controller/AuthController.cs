using Microsoft.AspNetCore.Mvc;
using Userdata.Models;
using Userdata.ViewModels;
using ExposeAPI.Utils;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using ExposeAPI.Auth;
namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
[ApiController]

public class AuthController : ControllerBase
{
        private readonly UserRepository userRepo;
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            this._configuration = configuration;
            userRepo = new UserRepository(Environment.GetEnvironmentVariable("ConnectionStrings") ?? configuration.GetConnectionString("Userdata"));
        }

        #region POST
        [HttpPost]
        [Route("Registration")]
        public async Task<ActionResult> Create(UserCreateDTO newItemDTO)
        {
            Logger log = new();
            log.LogAction("AuthController  Create");
            var newItemID = await userRepo.Create(newItemDTO);
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
            if (res !=null)
            {
                 authResponse = await AuthResponse.GenerateAuthResponse(res,_configuration);
            }
            return authResponse != null ? Ok(authResponse) : Unauthorized();
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenCustom jwtToken)
        {
            Logger log = new();
            log.LogAction("AuthController  RefreshToken");
            AuthenticationResponse? authResponse = null;
            if (string.IsNullOrWhiteSpace(jwtToken.access_token) || string.IsNullOrWhiteSpace(jwtToken.refresh_token))
            {
                return BadRequest("Access and refresh tokens must be both non-nullable and non-empty");
            }
            var tokenModule = new TokenAuthenticationModule();
            var principal = tokenModule.GetPrincipalFromExpiredToken(jwtToken.access_token,_configuration);
            if (principal != null)
            {
                var username = principal.Identity?.Name;
                var refreshTokenSaved = tokenModule.GenerateRefreshToken(username);
                var loggingUser = await userRepo.Get_user_Login(username);

                if (loggingUser != null && refreshTokenSaved == jwtToken.refresh_token)
                {
                    authResponse = await AuthResponse.GenerateAuthResponse(loggingUser, _configuration, true);
                }
            }
            return authResponse != null ? Ok(authResponse) : Unauthorized();
        }
        #endregion
        
        }
    
}
