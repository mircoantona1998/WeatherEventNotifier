using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Models;
using AppWeatherEventNotifier.Services.RestController;
using System.Collections.ObjectModel;


namespace AppWeatherEventNotifier.Services
{
    static class Refresh
    {
        //refresh dei feedback
        public static async Task refreshFeedbackUser()
        {  
            await TipController.getTipsUserLastHour();
        }
        public static async Task refreshInfoUser()
        {
            var res= await ConfigurationController.get_configurations((int)Convert.ToInt64(TodoItemDatabase.Instance.UserId));
            if (res!= null && res.Count > 0)
                Globals.configurationViewModel.Intentions_configurations = new ObservableCollection<Configuration>(res);
        }
    }
}
