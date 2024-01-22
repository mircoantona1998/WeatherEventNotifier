using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Models;
using AppWeatherEventNotifier.ViewModels;
using AppWeatherEventNotifier.Views.Registration;
using AppWeatherEventNotifier.Views.Splash;

namespace AppWeatherEventNotifier.Views.Login;

public partial class LoginPage : ContentPage
{
    private LoginViewModel _model;

    public LoginPage()
	{
		InitializeComponent();
        BindingContext = _model = new LoginViewModel();
        Navigation.PopToRootAsync();
        
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Globals.page_current = "Login";
        serverEntry.Text = Globals.server.Replace("http://","").Replace(":8080","");
        AppWeatherEventNotifier.Helper.Data.TodoItemDatabase database = await AppWeatherEventNotifier.Helper.Data.TodoItemDatabase.Instance;
        var x = await database.GetItemsAsync();
        foreach (TodoItem z in x)
        {
            if (z.Username != null)
                UsernameEntry.Text = z.Username;
            if (z.Password != null)
                PasswordEntry.Text = z.Password;
            if (z.Server != null)
                serverEntry.Text = z.Server.Replace("http://", "").Replace(":8080", "");
        }
        if (Connectivity.NetworkAccess == NetworkAccess.None)
        {
            await DisplayAlert("Attenzione", "Non sei connesso a internet", "OK");     
        }
    }
    private void CheckClicked(object sender, EventArgs e)
    {
        if (check.IsChecked)
        {
            check.IsChecked = false;
        }
        else check.IsChecked = true;
    }
    private async  void LoginClicked(object sender, EventArgs e)
    {
        activityController.turnOn();
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
        var x = await _model.OnLoginClicked();
        if (x == true)
        {
            if (check.IsChecked == true)
            {
                var x1 = await database.GetItemsAsync();
                foreach (TodoItem z in x1)
                {
                    if (z.Username != null)
                        await database.DeleteItemAsync(z);
                    if (z.Password != null)
                        await database.DeleteItemAsync(z);
                }

                var todoItem = new TodoItem();
                todoItem.Username = UsernameEntry.Text;
                todoItem.Password = PasswordEntry.Text;
                Helper.Data.TodoItemDatabase database1 = await Helper.Data.TodoItemDatabase.Instance;
                await database1.SaveItemAsync(todoItem);
            }
            activityController.turnOff();
            enabledAllButton();
           
             await Navigation.PushAsync(new SplashLogin());
        
        }
        else
        {
            activityController.turnOff();
            enabledAllButton();
            await DisplayAlert("Attenzione", "Login fallita", "Riprova");
        }
    }
    private void disabledAllButton()
    {
        login.IsEnabled = false;
        reg.IsEnabled = false;
    }
    private void enabledAllButton()
    {
        login.IsEnabled = true;
        reg.IsEnabled = true;
    }
    private async void RegisterClicked(object sender, EventArgs e)
    {
        Globals.server = "http://" + serverEntry.Text.Replace("http://", "").Replace(":8080", "") + ":8080";
        AppWeatherEventNotifier.Helper.Data.TodoItemDatabase database = await AppWeatherEventNotifier.Helper.Data.TodoItemDatabase.Instance;
        var x1 = await database.GetItemsAsync();
        foreach (TodoItem z in x1)
        {
            if (z.Server != null)
                await database.DeleteItemAsync(z);
        }

        var todoItem = new TodoItem();
        todoItem.Server = Globals.server;
        Helper.Data.TodoItemDatabase database1 = await Helper.Data.TodoItemDatabase.Instance;
        await database1.SaveItemAsync(todoItem);
        await Navigation.PushAsync(new RegisterPage());
    }

    private void ShowPassClicked(object sender, EventArgs e)
    {   
        if (PasswordEntry.IsPassword == false)
        {
            PasswordEntry.IsPassword = true;
            pass.Source = "eye.png";
        }
        else if (PasswordEntry.IsPassword == true)
        {
            PasswordEntry.IsPassword = false;
            pass.Source = "eyeopen.png";
        }
    }
 
}

