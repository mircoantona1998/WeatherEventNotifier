using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Models;
using AppWeatherEventNotifier.ViewModels;

namespace AppWeatherEventNotifier.Views.Home;

public partial class HomePage : ContentPage
{
    private HomeViewModel _model;

    public HomePage()
	{
		InitializeComponent();
        BindingContext = _model = new HomeViewModel();
 
    }
}

