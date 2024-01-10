using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Models;
using AppWeatherEventNotifier.Services.RestController;
using System.Collections.ObjectModel;


namespace AppWeatherEventNotifier.Services
{
    static class Refresh
    {
        //refresh dei feedback
        public static async Task refreshTips()
        {  
            var res=await TipController.getTips();
            if (res != null && res.Count > 0)
                Globals.configurationViewModel.Intentions_tips = new ObservableCollection<Tip>(res);
            else Globals.configurationViewModel.Intentions_tips = new ObservableCollection<Tip>();
        }
        public static async Task refreshInfoUser()
        {
            await refreshConfigurationUser();
            await refreshMailUser();
            await refreshTelegramUser();
            await refreshTips();
        }
        public static async Task refreshConfigurationUser()
        {
            var res = await ConfigurationController.get_configurations();
            if (res != null && res.Count > 0)
                Globals.configurationViewModel.Intentions_configurations = new ObservableCollection<Configuration>(res);
            else Globals.configurationViewModel.Intentions_configurations = new ObservableCollection<Configuration>();
        }
        public static async Task refreshTelegramUser()
        {
            var resUserTele = await TelegramController.get_user_telegram();
            if (resUserTele != null && resUserTele.Count > 0)
                Globals.userTelegram = resUserTele.FirstOrDefault();
            else Globals.userTelegram = null;
        }
        public static async Task refreshMailUser()
        {
            var resUserMail = await MailController.get_user_mail();
            if (resUserMail != null && resUserMail.Count > 0)
                Globals.userMail = resUserMail.FirstOrDefault();
            else Globals.userMail = null;
        }
    }
}
