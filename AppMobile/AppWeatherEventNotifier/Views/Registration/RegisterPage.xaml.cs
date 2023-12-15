using AppWeatherEventNotifier.Services.RestController;

namespace AppWeatherEventNotifier.Views.Registration;

public partial class RegisterPage : ContentPage
{

    public RegisterPage()
    {
        InitializeComponent();
    }
    private async void RegisterClicked(object sender, EventArgs e)
    {

        if(entryName.Text.Length >0 && entrySurname.Text.Length > 0 && entryUsername.Text.Length > 0 && entryPassword.Text.Length> 0 && entryCAP.Text.Length > 0 && entryCity.Text.Length > 0 && entryMail.Text.Length > 0 && entryTelefono.Text.Length>0 && entryAddress.Text.Length>0) { 
            if(entryMail.Text.Contains("@")==true) {
                bool x=await RegistrationController.registration_request(entryName.Text,entrySurname.Text, entryUsername.Text,entryPassword.Text, entryCAP.Text, entryCity.Text, entryMail.Text, entryTelefono.Text,entryAddress.Text);
                if (x)
                {
                    await DisplayAlert("Successo", "Registrazione completata", "Ok");
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                { 
                    await DisplayAlert("Attenzione", "Registrazione fallita", "Ok");
                }
            }else await DisplayAlert("Attenzione", "Inserisci una mail valida", "Ok");
        }
        else
        {
            await DisplayAlert("Attenzione", "Devi completare tutti i campi", "Ok");
        }
    }
}
