using AppWeatherEventNotifier.Services;
using AppWeatherEventNotifier.Models;
using AppWeatherEventNotifier.Services.RestController;

namespace AppWeatherEventNotifier.Views.Splash;

public partial class SplashLogin : ContentPage
{
    private IDispatcherTimer timer = Application.Current.Dispatcher.CreateTimer();
    public int i = 0;
    public int rotation = 0;
    // public int rotation_lab = 0;
    public SplashLogin()
	{
		InitializeComponent();
        name_user.Text ="Benvenuto, "+ AppWeatherEventNotifier.Helper.TodoItemDatabase.Instance.UsernameEntry+"!";    
    }
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
    protected async override  void OnAppearing()
    {
        base.OnAppearing();
        timer.Interval = TimeSpan.FromSeconds(0.4);
        timer.Tick += (s, e) => DoSomething();
        timer.Start();

        // await Refresh.refreshInfoUser();

        await PingController.mqtt_timer();
        App.Current.MainPage = new NavigationPage(new AppShell());

    }
    async void  DoSomething()
    {
        //if (img2.IsVisible == false) img2.IsVisible = true;
        //else img2.IsVisible = false;
        //App.Current.MainPage = new NavigationPage(new AppShell());

        // timer.Stop();
        rotation += 36;
       // rotation_lab += 5;
        if (i == 0)
        {

            img1.IsVisible = true;
            img2.IsVisible = false;
            img3.IsVisible = false;

            corsa1.IsVisible = true;
            corsa2.IsVisible = false;
            corsa3.IsVisible = false;
            await img1.RotateTo(rotation);
           // await lab.RotateTo(-5);
            i++;
        }
        else if (i == 1)
        {

            img1.IsVisible = false;
            img2.IsVisible = true;
            img3.IsVisible = false;

            corsa1.IsVisible = false;
            corsa2.IsVisible = true;
            corsa3.IsVisible = false;

            await img2.RotateTo(rotation);
            // await lab.RotateTo(+5);
            i++;
        }
        else if (i == 2)
        {

            img1.IsVisible = false;
            img2.IsVisible = false;
            img3.IsVisible = true;

            corsa1.IsVisible = false;
            corsa2.IsVisible = false;
            corsa3.IsVisible = true;
            await img3.RotateTo(rotation);
           // await lab.RotateTo(0);
            i = 0;
        }
    }

}

