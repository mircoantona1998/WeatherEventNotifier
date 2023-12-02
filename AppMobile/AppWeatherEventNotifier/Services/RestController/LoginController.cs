using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace AppWeatherEventNotifier.Services.RestController
{
     class LoginController
    {    
        public static async Task<bool> login_request(string usernameEntry, string passEntry)
        {
            string endPoint = "/auth/login";
            var body = new
            {
                Username = usernameEntry,
                Password = passEntry
            };
            LoginResponse res = await Helper.HttpHelper.HttpPostRequest<LoginResponse>(endPoint, body, false);
            if (res == null)
            {
                return false;
            }
            if (res?.Token != null)
            {
               
                TodoItemDatabase.Instance.Token = res.Token["access_token"];
                TodoItemDatabase.Instance.Refresh_Token = res.Token["refresh_token"];
                TodoItemDatabase.Instance.IsLoggedIn = true;
                TodoItemDatabase.Instance.Username = usernameEntry;
                TodoItemDatabase.Instance.UsernameEntry = usernameEntry;
                TodoItemDatabase.Instance.PasswordEntry = passEntry;
            }
            else
            {
                Globals.Logout();
            }
            if (res.success == true) { return true; }
            else return false;
        }
    }
}
