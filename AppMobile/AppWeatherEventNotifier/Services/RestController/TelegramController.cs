using AppWeatherEventNotifier.Models;

namespace AppWeatherEventNotifier.Services.RestController
{
    internal class TelegramController
    {
        public static async Task<List<Models.Metric>> get_metrics()
        {
            string endPoint = "";
            endPoint = "/Metric/Get";
            var res = await Helper.HttpHelper.HttpGetRequest<List<Metric>>(endPoint, true);
            return res;
        }
    }
}
