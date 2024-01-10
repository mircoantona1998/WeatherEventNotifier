using AppWeatherEventNotifier.Models;
using Microsoft.Maui.ApplicationModel.Communication;

namespace AppWeatherEventNotifier.Services.RestController
{
    internal class TelegramController
    {
        public static async Task<List<UserTelegram>> get_user_telegram()
        {
            string endPoint = "/UserTelegram/Get?All=true";
            var res = await Helper.HttpHelper.HttpGetRequest<List<UserTelegram>>(endPoint, true);
            return res;
        }
        public static async Task<bool> add_user_telegram(string chatID)
        {

            var body = new
            {
                ChatID = chatID
            };
            string endPoint = "/UserTelegram/Add";
            bool res = await Helper.HttpHelper.HttpPostRequest<bool>(endPoint, body, true);
            return res;
        }
        public static async Task<bool> patch_user_telegram(string chatID, bool isActive)
        {
            var dto = new
            {
                ChatID = chatID,
                IsActive=isActive
            };
            string endPoint = "/UserTelegram/Patch";
            bool res = await Helper.HttpHelper.HttpPatchRequest<bool>(endPoint,dto, true);
            return res;
        }
    }
}
