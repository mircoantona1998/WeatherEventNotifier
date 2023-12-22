using AppWeatherEventNotifier.ViewModels;
namespace AppWeatherEventNotifier.Views.Map
{
    public partial class MapPage : ContentPage
    {
        //private MapViewModel _model;

        public MapPage()
        {
            InitializeComponent();
            //_model = new MapViewModel();
            //BindingContext = _model;

            mappy.Pins.Add(new Microsoft.Maui.Controls.Maps.Pin
            {
                Label = "Subscribe to my channel?",
                Location = new Location(50.8514, 5.6910),
            });
        }

        protected async override void OnAppearing()
        {
            await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

        }
    }

}
