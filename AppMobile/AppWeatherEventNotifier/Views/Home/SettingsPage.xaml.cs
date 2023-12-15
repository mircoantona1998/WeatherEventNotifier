using AppWeatherEventNotifier.Helper;
namespace AppWeatherEventNotifier.Views.HomePage;

public partial class SettingsPage : ContentPage
{

    public SettingsPage()
    {
        InitializeComponent();
    }
    private void logout(object sender, EventArgs e)
    {
        Globals.Logout();
        AppWeatherEventNotifier.App.Current.MainPage = new NavigationPage(new AppWeatherEventNotifier.Views.Login.LoginPage());
    }

    private void RefreshClicked(object sender, EventArgs e)
    {
    }

    private async void Button_Modifica(object sender, EventArgs e)
    {
        await DisplayAlert("Attenzione","Non hai modificato nulla","Ok");
    }
}
