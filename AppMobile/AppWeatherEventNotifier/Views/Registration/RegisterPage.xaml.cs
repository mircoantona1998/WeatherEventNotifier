namespace AppWeatherEventNotifier.Views.Registration;

public partial class RegisterPage : ContentPage
{

    public RegisterPage()
    {
        InitializeComponent();
        var navigationPage = Application.Current.MainPage as NavigationPage;
        navigationPage.Title = "Registrazione";
    }
    private void RegisterClicked(object sender, EventArgs e)
    {
        
    }
}
