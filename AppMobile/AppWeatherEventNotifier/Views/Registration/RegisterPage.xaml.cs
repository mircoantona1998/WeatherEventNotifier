using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Models;
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
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        AppWeatherEventNotifier.Helper.Data.TodoItemDatabase database = await AppWeatherEventNotifier.Helper.Data.TodoItemDatabase.Instance;
        var x = await database.GetItemsAsync();
        foreach (TodoItem z in x)
        {
            if (z.Server != null)
                serverEntry.Text = z.Server.Replace("http://", "").Replace(":8080", "");
        }
    }
    private async void RegisterClicked(object sender, EventArgs e)
    {
        disabledAllButton();
        Globals.server = "http://" + serverEntry.Text.Replace("http://", "").Replace(":8080", "") + ":8080";
        AppWeatherEventNotifier.Helper.Data.TodoItemDatabase database = await AppWeatherEventNotifier.Helper.Data.TodoItemDatabase.Instance;
        var x11 = await database.GetItemsAsync();
        foreach (TodoItem z in x11)
        {
            if (z.Server != null)
                await database.DeleteItemAsync(z);
        }

        var todoItem1 = new TodoItem();
        todoItem1.Server = Globals.server;
        Helper.Data.TodoItemDatabase database11 = await Helper.Data.TodoItemDatabase.Instance;
        await database11.SaveItemAsync(todoItem1);
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
