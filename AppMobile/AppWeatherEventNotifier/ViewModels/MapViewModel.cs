using AppWeatherEventNotifier.Models;
using System.Collections.ObjectModel;


namespace AppWeatherEventNotifier.ViewModels;

public class MapViewModel : BaseViewModel
{
    private double latitude;
    private double longitude;

    public double Latitude
    {
        get { return latitude; }
        set
        {
            latitude = value;
            OnPropertyChanged(nameof(Latitude));
        }
    }

    public double Longitude
    {
        get { return longitude; }
        set
        {
            longitude = value;
            OnPropertyChanged(nameof(Longitude));
        }
    }

    public Command SelectLocationCommand { get; }

    public MapViewModel()
    {
        SelectLocationCommand = new Command(OnSelectLocation);
    }

    private async void OnSelectLocation()
    {
        var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default));
        if (location != null)
        {
            Latitude = location.Latitude;
            Longitude = location.Longitude;
        }
        else
        {
        }
    }

}