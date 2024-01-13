using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Services.RestController;

namespace AppWeatherEventNotifier.Views.Registration;

public partial class RegisterPage : ContentPage
{

    public RegisterPage()
    {
        InitializeComponent();
        serverEntry.Text = Globals.server.Replace("http://", "").Replace(":8080", "");
    }
    private void disabledAllButton()
    {
        activityController.turnOn();
        register.IsEnabled = false;      
    }
    private void enabledAllButton()
    {
        activityController.turnOff();
        register.IsEnabled = true;
    }
    private async void RegisterClicked(object sender, EventArgs e)
    {
        disabledAllButton();
        Globals.server = "http://" + serverEntry.Text.Replace("http://", "").Replace(":8080", "") + ":8080";
        if (entryName.Text != null && entryName.Text.Trim() != "" && entrySurname.Text != null && entrySurname.Text.Trim() != "" && entryUsername.Text != null && entryUsername.Text.Trim() != "" && entryPassword.Text != null && entryPassword.Text.Trim() != "" && entryCAP.Text != null && entryCAP.Text.Trim() != "" && entryCity.Text != null && entryCity.Text.Trim() != "" && entryAddress.Text != null && entryAddress.Text.Trim() != "") { 
            bool x=await RegistrationController.registration_request(entryName.Text,entrySurname.Text, entryUsername.Text,entryPassword.Text, entryCAP.Text, entryCity.Text,entryAddress.Text);
            if (x)
            {
                await DisplayAlert("Successo", "Registrazione completata", "Ok");
                await App.Current.MainPage.Navigation.PopAsync();
                enabledAllButton();
            }
            else
            { 
                await DisplayAlert("Attenzione", "Registrazione fallita", "Ok");
            }
        }
        else
        {
            await DisplayAlert("Attenzione", "Devi completare tutti i campi", "Ok");
        }
        enabledAllButton();
    }
}
