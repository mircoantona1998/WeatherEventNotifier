using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Services.RestController;

namespace AppWeatherEventNotifier.Views.HomePage;

public partial class AlertConfigurationPage : ContentPage
{

    public AlertConfigurationPage()
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

    private async void Button_Clicked_Get(object sender, EventArgs e)
    {
       var res= await ConfigurationController.get_configurations((int)Convert.ToInt64( TodoItemDatabase.Instance.UserId));
        Console.WriteLine(res);
    }
    private async void Button_Clicked_Add(object sender, EventArgs e)
    {
        var res = await ConfigurationController.add_configuration((int)Convert.ToInt64(TodoItemDatabase.Instance.UserId),23,23,"metric","frequency");
        Console.WriteLine(res);
    }
    private async void Button_Clicked_Modify(object sender, EventArgs e)
    {
        var res = await ConfigurationController.patch_configuration(6,(int)Convert.ToInt64(TodoItemDatabase.Instance.UserId), 23, 23, "metric", "frequency");
        Console.WriteLine(res);
    }
    private async void Button_Clicked_Delete(object sender, EventArgs e)
    {
        var res = await ConfigurationController.delete_configuration((int)Convert.ToInt64(TodoItemDatabase.Instance.UserId),2);
        Console.WriteLine(res);
    }
}
