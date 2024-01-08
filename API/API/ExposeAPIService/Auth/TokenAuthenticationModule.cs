using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static ExposeAPI.Controllers.AuthController;

namespace ExposeAPI.Auth
{
    public class TokenAuthenticationModule
    {
        public string Username;
        public string Surname;
        public string Name;
        public int Id;
        public bool isAdmin;
        public string[] Roles;
        public string TokenString;
        public string RefreshRandomToken = String.Empty;
        public JwtSecurityToken jwtTokenSec;
        public int TimeTokenValidity = 20;

        /// <summary>
        /// TokenAuthenticationModule
        /// </summary>
        public TokenAuthenticationModule()
        {

        }

        /// <summary>
        /// TokenCreation
        /// </summary>
        public JwtSecurityToken TokenCreation(IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var secretKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            Claim ClaimIdentityName = new Claim(ClaimTypes.Name, Username);
            Claim ClaimId = new Claim("Id", Id.ToString());

            ClaimsIdentity myClaimIdentity = new ClaimsIdentity();
            myClaimIdentity.AddClaim(ClaimIdentityName);
            myClaimIdentity.AddClaim(ClaimId);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = myClaimIdentity,
                Expires = DateTime.Now.AddMinutes(TimeTokenValidity),
                SigningCredentials = signinCredentials
            };

            //Output
            jwtTokenSec = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            TokenString = new JwtSecurityTokenHandler().WriteToken(jwtTokenSec);

            return jwtTokenSec;
        }

        /// <summary>
        /// GenerateRefreshToken
        /// </summary>
        /// <returns></returns>
        public string GenerateRefreshToken(string Username)
        {
            if (!(string.IsNullOrEmpty(Username)))
            {
                byte[] randomNumber = Encoding.ASCII.GetBytes(Username);

                RefreshRandomToken = Convert.ToBase64String(randomNumber);
                return RefreshRandomToken;
            }

            return String.Empty;
        }

        /// <summary>
        /// GetPrincipalFromExpiredToken
        /// </summary>
        /// <param name="token"></param>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration configuration)
        {
            var secretKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var Key = secretKey;
            ClaimsPrincipal principal = null;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = secretKey,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                //Check Token
                var tokenHandler = new JwtSecurityTokenHandler();
                principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out Microsoft.IdentityModel.Tokens.SecurityToken securityToken);
                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
     
    }

}
