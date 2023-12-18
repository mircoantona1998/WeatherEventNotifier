using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Services;
using AppWeatherEventNotifier.Views.Configuration;

namespace AppWeatherEventNotifier.Views.HomePage;

public partial class ManageConfigurationsPage : ContentPage
{
    private bool canOpen = true;
    public ManageConfigurationsPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        configurationsCollectionView.ItemsSource = Globals.configurationViewModel.Intentions_configurations;
    }
    private void logout(object sender, EventArgs e)
    {
        Globals.Logout();
        AppWeatherEventNotifier.App.Current.MainPage = new NavigationPage(new AppWeatherEventNotifier.Views.Login.LoginPage());
    }

    private async void RefreshClicked(object sender, EventArgs e)
    {
        disableAll();
        await Refresh.refreshInfoUser();
        configurationsCollectionView.ItemsSource = Globals.configurationViewModel.Intentions_configurations;
        enableAll();
    }
    private async void Frame_Tapped(object sender, EventArgs e)
    {
        if (canOpen == true)
        {
            canOpen = false;
            Globals.ConfigurationSelected= ((Frame)sender).BindingContext as Models.Configuration;
            await Navigation.PushAsync(new ConfigurationPage());
            canOpen = true;
        }
    }
    private async void Button_Clicked_Add(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddConfigurationPage());
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
