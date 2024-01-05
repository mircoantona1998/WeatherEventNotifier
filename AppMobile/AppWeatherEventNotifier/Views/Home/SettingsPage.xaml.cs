using AppWeatherEventNotifier.Helper;
namespace AppWeatherEventNotifier.Views.HomePage;

public partial class SettingsPage : ContentPage
{
    bool modificaEnable = false;
    public SettingsPage()
    {
        InitializeComponent();
        if (modificaEnable == false)
        {
            modificaButton.Text = "Modifica";
            modificaEnable = false;
            TelegramSwitch.IsEnabled = false;
            MailSwitch.IsEnabled = false;
            Mail.IsEnabled = false;
            Telegram.IsEnabled = false;
        }
        else
        {
            modificaEnable = true;
            TelegramSwitch.IsEnabled = true;
            MailSwitch.IsEnabled = true;
            Mail.IsEnabled = true;
            Telegram.IsEnabled = true;
            modificaButton.Text = "Invia modifica";
        }
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
        if(modificaEnable==false)
        {
            modificaButton.Text = "Invia modifica";
            modificaEnable = true;
            TelegramSwitch.IsEnabled = true;
            MailSwitch.IsEnabled = true;
            Mail.IsEnabled = true;
            Telegram.IsEnabled = true;
        }
        else
        {
            modificaEnable = false;
            TelegramSwitch.IsEnabled = false;
            MailSwitch.IsEnabled = false;
            Mail.IsEnabled = false;
            Telegram.IsEnabled = false;
            modificaButton.Text = "Modifica";
            await DisplayAlert("Attenzione", "Non hai modificato nulla", "Ok");
        }
        
    }
}
