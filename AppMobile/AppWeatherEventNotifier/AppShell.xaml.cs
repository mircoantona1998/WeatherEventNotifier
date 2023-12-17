using AppWeatherEventNotifier.Helper;


namespace AppWeatherEventNotifier;

public partial class AppShell : Shell
{
    public AppShell()
	{
		InitializeComponent();
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
