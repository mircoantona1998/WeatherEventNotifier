using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Models;
using AppWeatherEventNotifier.Views.Login;

namespace AppWeatherEventNotifier.Services.RestController
{
     class TipController
     {    
        public static async Task<List<Tip>> getTips()
        {
            string endPoint = "/Notifier/Get";
            var res = await Helper.HttpHelper.HttpGetRequest<List<Tip>>(endPoint, true);               
            return res;       
        }
     }
}
