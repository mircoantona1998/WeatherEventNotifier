using CommunityToolkit.Maui.Views;
using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Services.Events;
using AppWeatherEventNotifier.Views;
using AppWeatherEventNotifier.Views.Login;
using AppWeatherEventNotifier.Views.Splash;
using Microsoft.Maui.Platform;

namespace AppWeatherEventNotifier;

public partial class App : Application
{
    IDispatcherTimer timerToken;
    IDispatcherTimer timerMqtt;
    public App()
    {
        InitializeComponent();

        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjU3MTM3M0AzMjMyMmUzMDJlMzBNT3d2NVpkVlJ3dktXZXl1M1diZitHUlVDU3BWaHlnazlwbkRBYS9UNFBzPQ==");
 

        MainPage = new NavigationPage(new LoginPage());
    }
    public static async Task<bool> TaskRefreshToken()
    {

        try
        {
            Globals.lastRefreshTokenRequest = DateTime.Now;
            if (TodoItemDatabase.Instance.IsLoggedIn == true)
            {
                bool res=await Services.RestController.TokenController.refresh_token();
                return res;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    public static async Task<bool> TaskMqtt()
    {
        try
        {
            Globals.lastMqttRequest = DateTime.Now;
            if (TodoItemDatabase.Instance.IsLoggedIn == true)
            {
            await Services.RestController.PingController.mqtt_timer();             
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
    protected override void OnSleep()
    {
        if (Globals.page_current == "MainPage") 
        { 
            object sender=new object();
            EventArgs e=new EventArgs();      
        }

        timerMqtt.Stop();
        timerToken.Stop();
    }

    protected override async void OnResume() {
        timerToken.Start();
        timerMqtt.Start();

        if (TodoItemDatabase.Instance.IsLoggedIn == true)
        {     
            TimeSpan differences = DateTime.Now - Globals.lastRefreshTokenRequest;
            if (differences.TotalMinutes > 3) { 
              bool res=await TaskRefreshToken();
                if(res==true)
                    EventRefresh.RunEvents(null, null);
            }
            differences = DateTime.Now - Globals.lastMqttRequest;
            if (differences.TotalMinutes> 3)
                await TaskMqtt();           
        }
    }
}
