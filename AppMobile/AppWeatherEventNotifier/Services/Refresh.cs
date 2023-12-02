using AppWeatherEventNotifier.Services.RestController;


namespace AppWeatherEventNotifier.Services
{
    static class Refresh
    {
        //refresh dei feedback
        public static async Task refreshFeedbackUser()
        {  
            //notifiche dell'utente ultima ora
            await TipController.getTipsUserLastHour();
        }
    }
}
