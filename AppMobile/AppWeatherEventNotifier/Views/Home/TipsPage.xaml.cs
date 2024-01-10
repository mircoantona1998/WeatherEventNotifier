using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Services;
using System.Windows.Input;
namespace AppWeatherEventNotifier.Views.HomePage;
public partial class TipsPage : ContentPage
{
    public TipsPage()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        disableAll();
        await Refresh.refreshTips();
        tipsCollectionView.ItemsSource = Globals.configurationViewModel.Intentions_tips;
        enableAll();
    }
    private void logout(object sender, EventArgs e)
    {
        Globals.Logout();
        AppWeatherEventNotifier.App.Current.MainPage = new NavigationPage(new AppWeatherEventNotifier.Views.Login.LoginPage());
    }

    private async void RefreshClicked(object sender, EventArgs e)
    {
        disableAll();
        await Refresh.refreshTips();
        tipsCollectionView.ItemsSource = Globals.configurationViewModel.Intentions_tips;
        enableAll();
    }
    private void disableAll()
    {
        activityController.turnOn();

    }
    private void enableAll()
    {
        activityController.turnOff();
    }

}