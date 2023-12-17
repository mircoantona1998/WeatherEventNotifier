using AppWeatherEventNotifier.ViewModels;
namespace AppWeatherEventNotifier.Views.Map
{
    public partial class MapPage : ContentPage
    {
        private MapViewModel _model;

        public MapPage()
        {
            //InitializeComponent();
            _model = new MapViewModel();
            BindingContext = _model;
        }
    }
    
}
