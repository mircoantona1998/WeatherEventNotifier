using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Services;
using AppWeatherEventNotifier.Services.RestController;
namespace AppWeatherEventNotifier.Views.HomePage;

public partial class SettingsPage : ContentPage
{
    bool modificaEnable = false;
    public SettingsPage()
    {
        InitializeComponent();

        if (Globals.userTelegram != null) { 
            Telegram.Text = Globals.userTelegram.ChatId;
            TelegramSwitch.IsToggled = (bool)Globals.userTelegram.IsActive;
        }else
        {
            Telegram.Text = "";
            TelegramSwitch.IsToggled = false;
        }

        if (Globals.userMail != null)
        {
            Mail.Text=Globals.userMail.Mail;
            MailSwitch.IsToggled = (bool)Globals.userMail.IsActive;
        }
        else
        {
            Mail.Text = "";
            MailSwitch.IsToggled = false;
        }
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
    private void disableAll()
    {
        activityController.turnOn();

    }
    private void enableAll()
    {
        activityController.turnOff();
    }
    private async void Button_Modifica(object sender, EventArgs e)
    {
        disableAll();
        if ( modificaEnable==false )
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
            if (Telegram.Text != "") 
            { 
                var res=await TelegramController.patch_user_telegram(Telegram.Text,TelegramSwitch.IsToggled);
                if(res!=true) {
                    await DisplayAlert("Attenzione", "Modifica recapito di telegram NON riuscita", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Attenzione", "Modifica recapito di telegram NON riuscita", "Ok");
            }
            if (Mail.Text != "")
            {
                var res = await MailController.patch_user_mail(Mail.Text, MailSwitch.IsToggled);
                if (res != true)
                {
                    await DisplayAlert("Attenzione", "Modifica recapito di mail NON riuscita", "Ok");
                }
            }
            else await DisplayAlert("Attenzione", "Modifica recapito di mail NON riuscita", "Ok");

            modificaEnable = false;
            TelegramSwitch.IsEnabled = false;
            MailSwitch.IsEnabled = false;
            Mail.IsEnabled = false;
            Telegram.IsEnabled = false;
            modificaButton.Text = "Modifica";

            await Refresh.refreshTelegramUser();
            await Refresh.refreshMailUser();

            if (Globals.userTelegram != null)
            {
                Telegram.Text = Globals.userTelegram.ChatId;
                TelegramSwitch.IsToggled = (bool)Globals.userTelegram.IsActive;
            }
            else
            {
                Telegram.Text = "";
                TelegramSwitch.IsToggled = false;
            }

            if (Globals.userMail != null)
            {
                Mail.Text = Globals.userMail.Mail;
                MailSwitch.IsToggled = (bool)Globals.userMail.IsActive;
            }
            else
            {
                Mail.Text = "";
                MailSwitch.IsToggled = false;
            }
            
        }
        enableAll();
    }

    private async void OnTelegramLinkClicked(object sender, EventArgs e)
    {
        var telegramChannel = "WeatherEventNotifierBot";
        var uri = new Uri($"tg://resolve?domain={telegramChannel}");
        try
        {
            await Launcher.OpenAsync(uri);
        }
        catch (Exception ex)
        {
            try
            {
                var telegramLink = "https://t.me/WeatherEventNotifierBot";
                await Launcher.OpenAsync(telegramLink);
            }
            catch (Exception ex1)
            {
                await DisplayAlert("Attenzione","Impossibile aprire telegram","Ok");
            }
        }
    }
}
