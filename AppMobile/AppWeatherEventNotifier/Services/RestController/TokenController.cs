using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Models;
using AppWeatherEventNotifier.Views.Login;

namespace AppWeatherEventNotifier.Services.RestController
{
     class TokenController
     {    
        public static async Task<bool> refresh_token()
        {        
            string endPoint = "/auth/refresh-token";
            var body = new
            {
                access_token = TodoItemDatabase.Instance.Token,
                refresh_token = TodoItemDatabase.Instance.Refresh_Token
            };
            RefreshTokenResponse res = await Helper.HttpHelper.HttpPostRequest<RefreshTokenResponse>(endPoint, body, true);
            if (res?.Token != null)
            {

                TodoItemDatabase.Instance.Token = res.Token["access_token"];
                TodoItemDatabase.Instance.Refresh_Token = res.Token["refresh_token"];
                TodoItemDatabase.Instance.IsLoggedIn = true;
                return true;
            }

           return false;
        }
     }
}
