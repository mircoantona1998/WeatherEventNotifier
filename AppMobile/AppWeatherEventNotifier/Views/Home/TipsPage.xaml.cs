
using AppWeatherEventNotifier.Helper;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace AppWeatherEventNotifier.Views.HomePage;
public partial class TipsPage : ContentPage
{

    public TipsPage()
    {
        InitializeComponent();
        create_command_refresh();
        //EventRefresh.EventRefresh1 += refresh_event;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Dispatcher.Dispatch(async () => await refresh());
    }
    private void create_command_refresh()
    {
        ICommand refreshCommand = new Command(async () =>
        {
            refreshView.IsRefreshing = false;
            await refresh();
        });
        refreshView.Command = refreshCommand;
    }
    private void logout(object sender, EventArgs e)
    {
        Globals.Logout();
        AppWeatherEventNotifier.App.Current.MainPage = new NavigationPage(new AppWeatherEventNotifier.Views.Login.LoginPage());
    }

    private void RefreshClicked(object sender, EventArgs e)
    {
    }
    private async Task refresh()
    {
        //if (Globals.finish_incentive_refresh == false)
        //    return;
        //Globals.finish_incentive_refresh = false;
        //activityController.turnOn();
        //await Refresh.refreshFeedbackUser();
        //if (TodoItemDatabase.Instance.TipsUser == null || TodoItemDatabase.Instance.TipsUser.Count == 0)
        //{
        //    NoFeed.IsVisible = true;
        //    Globals.homeViewModel.Intentions_tips = new ObservableCollection<Tip>();
        //}
        //else
        //{
        //    NoFeed.IsVisible = false;
        //    Globals.homeViewModel.Intentions_tips = new ObservableCollection<Tip>(TodoItemDatabase.Instance.TipsUser);
        //}
        //activityController.turnOff();
        //Globals.finish_incentive_refresh = true;
    }
    private async void turnOn(object sender, System.EventArgs e)
    {
        string server = Globals.server;
        Globals.server = "https://interconnectservice.azurewebsites.net/service";
        string endPoint = "/delay";
        var body = new
        {
            sequenceId = 1,
            deviceAddress = 1,
            playerId = 1,
            startTime = DateTime.UtcNow,
        };
        //LoginResponse res = await Helper.HttpHelper.HttpPostRequest<LoginResponse>(endPoint, body, false);
        //if (res == null)
        //{
        //}
        Globals.server = server;
    }
    private async void turnOff(object sender, System.EventArgs e)
    {
        string server = Globals.server;
        Globals.server = "https://interconnectservice.azurewebsites.net/service";
        string endPoint = "/delay";
        var body = new
        {
            sequenceId = 1,
            deviceAddress = 1,
            playerId = 1,
            startTime = DateTime.UtcNow,
        };
        //LoginResponse res = await Helper.HttpHelper.HttpPostRequest<LoginResponse>(endPoint, body, false);
        // if (res == null)
        // {
        // }
        Globals.server = server;
    }
    private async void refresh_event(object sender, System.EventArgs e)
    {
        await refresh();
    }
}