using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Services;
using AppWeatherEventNotifier.Services.Events;
using AppWeatherEventNotifier.ViewModels;
using Microsoft.Maui.Controls;

namespace AppWeatherEventNotifier;

public partial class AppShell : Shell
{
    private HomeViewModel _model;
    public AppShell()
	{
		InitializeComponent();
        BindingContext = _model = new HomeViewModel();
        Globals.homeViewModel = _model;
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
    
    private void OnMenuItemClicked(object sender, EventArgs e)
    {
        Globals.Logout();
        AppWeatherEventNotifier.App.Current.MainPage = new NavigationPage(new AppWeatherEventNotifier.Views.Login.LoginPage());
    }

    //private async void Refresh_Clicked(object sender, EventArgs e)
    //{
    //    refre.IsEnabled = false;
    //    await Refresh.refreshInfoUser();
    //    _model.refreshData();
    //    refre.IsEnabled = true;
    //}
}
