using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Models;
using AppWeatherEventNotifier.Services.Events;




namespace AppWeatherEventNotifier.Services.RestController
{
     class PingController
    {
        public static async Task ping_timer()
        {
            try
            {              

                if (Globals.server != "")
                {
                    string endPoint = "/Ping";
                    PingResponse res = await Helper.HttpHelper.HttpGetRequest<PingResponse>(endPoint, false);
                    if (res != null) { Globals.IsConnected = true; }
                    else
                    {
                        if (Globals.page_current!="Server" && Globals.IsConnected==true)
                        {
                            await AppWeatherEventNotifier.App.Current.MainPage.DisplayAlert("Attenzione", "Non sei connesso al server", "OK");
                            Globals.Logout();
                            Globals.IsConnected = false;
                        }
                    }
                }
                else
                {
                    if (Globals.page_current != "Server" && Globals.IsConnected == true)
                    {
                        await AppWeatherEventNotifier.App.Current.MainPage.DisplayAlert("Attenzione", "Non sei connesso al server", "OK");
                        Globals.Logout();
                        Globals.IsConnected = false;
                    }
                }
                
                return;
            }
            catch
            {
                if (Globals.page_current != "Server" && Globals.IsConnected == true)
                {
                    await AppWeatherEventNotifier.App.Current.MainPage.DisplayAlert("Attenzione", "Non sei connesso al server", "OK");
                    Globals.Logout();
                    Globals.IsConnected = false;
                }
            }

        }
        public static async Task mqtt_timer()
        {
            try
            {

                    string endPoint = "/MqttStatus";
                //    MqttStatusResponse res = await Helper.HttpHelper.HttpGetRequest<MqttStatusResponse>(endPoint,true);
                    //if (res != null && res.MqttOk==true) {

                    //if (Globals.MqttStatus == false)
                    //{
                    //    Globals.MqttStatus = true;
                    //    object o = new object();
                    //    EventArgs e = new EventArgs();

                    //}
                    //}
                    //else
                    //{
                    //if (Globals.MqttStatus == true)
                    //{
                    //    Globals.MqttStatus = false;
                    //    object o = new object();
                    //    EventArgs e = new EventArgs();

                    //}
                    //}              
                return;
            }
            catch
            {
                //if (Globals.MqttStatus == true)
                //{
                //    Globals.MqttStatus = false;
                //    object o = new object();
                //    EventArgs e = new EventArgs();
                //    EventStatus.RunEvents(o, e);
                //}
            }

        }
        public static async Task ping_timer_server_page()
        {
            try
            {

                if (Globals.server != "")
                {
                    string endPoint = "/Ping";
                    PingResponse res = await Helper.HttpHelper.HttpGetRequest<PingResponse>(endPoint, false);
                    if (res != null) { Globals.IsConnected = true; }
                    else
                    {
                        Globals.Logout();
                        Globals.IsConnected = false;
                    }
                }
                else
                {
                    Globals.Logout();
                    Globals.IsConnected = false;
                }

                return;
            }
            catch
            {
                Globals.Logout();
                Globals.IsConnected = false;
            }

        }

    }
}
