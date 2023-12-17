using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Services;
using AppWeatherEventNotifier.Services.RestController;
using AppWeatherEventNotifier.Views.Configuration;

namespace AppWeatherEventNotifier.Views.HomePage;

public partial class ManageConfigurationsPage : ContentPage
{
    private bool canOpen = true;
    public ManageConfigurationsPage()
    {
        InitializeComponent();
        configurationsCollectionView.ItemsSource = Globals.configurationViewModel.Intentions_configurations;
    }
    private void logout(object sender, EventArgs e)
    {
        Globals.Logout();
        AppWeatherEventNotifier.App.Current.MainPage = new NavigationPage(new AppWeatherEventNotifier.Views.Login.LoginPage());
    }

    private async void RefreshClicked(object sender, EventArgs e)
    {
        await Refresh.refreshInfoUser();
        configurationsCollectionView.ItemsSource = Globals.configurationViewModel.Intentions_configurations;
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

    //private async void Button_Clicked_Get(object sender, EventArgs e)
    //{
    //   var res= await ConfigurationController.get_configurations((int)Convert.ToInt64( TodoItemDatabase.Instance.UserId));
    //    Console.WriteLine(res);
    //}
    private async void Button_Clicked_Add(object sender, EventArgs e)
    {
        //var res = await ConfigurationController.add_configuration((int)Convert.ToInt64(TodoItemDatabase.Instance.UserId),23,23,"metric","frequency");
        await Navigation.PushAsync(new AddConfigurationPage());
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
