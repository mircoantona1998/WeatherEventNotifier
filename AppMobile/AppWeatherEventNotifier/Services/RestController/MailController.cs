using AppWeatherEventNotifier.Models;

namespace AppWeatherEventNotifier.Services.RestController
{
    internal class MailController
    {
        public static async Task<List<UserMail>> get_user_mail()
        {
            string endPoint = "/UserMail/Get?All=true";
            var res = await Helper.HttpHelper.HttpGetRequest<List<UserMail>>(endPoint, true);
            return res;
        }
        public static async Task<bool> add_user_mail(string mail)
        {
            var dto = new
            {
                Mail = mail
            };
            string endPoint = "/UserMail/Add";
            bool res = await Helper.HttpHelper.HttpPostRequest<bool>(endPoint, dto, true);
            return res;
        }
        public static async Task<bool> patch_user_mail(string mail, bool isActive)
        {
            var dto = new
            {
                Mail = mail,
                IsActive = isActive
            };
            string endPoint = "/UserMail/Patch";
            bool res = await Helper.HttpHelper.HttpPatchRequest<bool>(endPoint, dto, true);
            return res;
        }
    }
}
