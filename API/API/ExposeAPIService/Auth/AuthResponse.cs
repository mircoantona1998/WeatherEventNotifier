using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace ExposeAPI.Auth
{
    public static class AuthResponse
    {
        public static async Task<AuthenticationResponse?> GenerateAuthResponse(IdentityUser loggingUser, IConfiguration _configuration, bool? isRefreshing = null)
        {
            AuthenticationResponse? authResponse = null;
            try
            {
                await Task.Run(() =>
                {
                    var tokenModule = new TokenAuthenticationModule
                    {
                        Id = (int)Convert.ToInt64(loggingUser.Id),
                        Username = loggingUser.UserName,

                    };
                    var accessToken = tokenModule.TokenCreation(_configuration);
                    var refreshToken = tokenModule.GenerateRefreshToken(loggingUser.UserName);
                    authResponse = new AuthenticationResponse
                    {
                        success = true,
                        token = new TokenCustom
                        {
                            access_token = new JwtSecurityTokenHandler().WriteToken(accessToken),
                            refresh_token = refreshToken,
                            idUser = (int)Convert.ToInt64(loggingUser.Id),
                        },
                        message = $"The user {loggingUser.UserName} is " + (isRefreshing == true ? "re-" : string.Empty) + "logged in ",
                        httpcode = 200,
                        ErrorCode = 0
                    };
                });
            }
            catch (Exception ex)
            {
            }
            return authResponse;
        }
    }
}
