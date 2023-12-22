using AppWeatherEventNotifier.Models;
using AppWeatherEventNotifier.Services;
using AppWeatherEventNotifier.Services.RestController;
using AppWeatherEventNotifier.ViewModels;


namespace AppWeatherEventNotifier.Views.Configuration;

 public partial class AddConfigurationPage : ContentPage
{
    private ConfigurationViewModel _model;
    private List<Metric> listMetric;
    private List<Frequency> frequencyList;
    private Frequency frequencySelected;
    private Metric metricSelected;

    public AddConfigurationPage()
    {
        InitializeComponent();
        BindingContext = _model = new ConfigurationViewModel();
        data_attivazione.MinimumDate = DateTime.Today;
    }
    private void Tapped(object sender)
    {
        // e.Position contains the tapped position
        //double latitude = e.Position.Latitude;
        //double longitude = e.Position.Longitude;

        //// Use the coordinates as needed (e.g., display in UI, perform further actions)
        //DisplayAlert("Tapped", $"Latitude: {latitude}, Longitude: {longitude}", "OK");
    }
    protected override async void OnAppearing()
    {
        disableAll();
        listMetric = await MetricController.get_metrics();
        if (listMetric != null)
        {
            Metrica.ItemsSource = listMetric;
            Metrica.ItemDisplayBinding = new Binding("Field");
        }
        frequencyList = await FrequencyController.get_frequencys();
        if (frequencyList != null)
        {
            Frequenza.ItemsSource = frequencyList;
            Frequenza.ItemDisplayBinding = new Binding("FrequencyName");
        }
        enableAll();
    }
    private void OnPickerSelectedIndexChangedMetric(object sender, EventArgs e)
    {
        metricSelected = (Metric)((Picker)sender).SelectedItem;
    }
    private void OnPickerSelectedIndexChangedFrequency(object sender, EventArgs e)
    {
        frequencySelected = (Frequency)((Picker)sender).SelectedItem;
    }
    private async void SaveConfigurationClicked(object sender, EventArgs e)
    {
        disableAll();
        var res= await OnValidateClicked();
        if (res == false) {
            enableAll();
            return; 
        }
        if (frequencySelected!=null && metricSelected != null && Longitudine.Text.Replace(".",",")!="" && Latitudine.Text.Replace(".", ",") != "" && Simbolo.SelectedItem !=null && Valore.Text!="")
        { 
            int? IdFrequency = frequencySelected.Id;
            float Longitude;
            if (float.TryParse(Longitudine.Text.Replace(".", ","), out Longitude))
            {
            }
            else
            {
                await DisplayAlert("Error", "Longitudine non valida", "Ok");
                return;
            }
            float Latitude;
            if (float.TryParse(Latitudine.Text.Replace(".", ","), out Latitude))
            {
            }
            else
            {
                await DisplayAlert("Error", "Longitudine non valida", "Ok");
                return;
            }
            int? IdMetric = metricSelected.Id;
            string Symbol = Simbolo.SelectedItem.ToString();
            float Value = Convert.ToInt64(Valore.Text.ToString());
            DateTime? dateTimeAttivazione = data_attivazione.Date.Add(time_attivazione.Time).ToUniversalTime();

            var resp = await ConfigurationController.add_configuration( Longitude, Latitude, IdMetric, IdFrequency, Symbol, Value, dateTimeAttivazione);
            if (resp == true)
            {
                await DisplayAlert("Successo", "Configurazione creata correttamente", "Ok");
                await Refresh.refreshInfoUser();
            }
            else
            {
                await DisplayAlert("Error", "Errore nella creazione della configurazione", "Ok");
            }
        }
        else
        {
            await DisplayAlert("Attenzione", "Devi compilare tutti i campi", "Ok");
            enableAll();
            return;
        }
        await Navigation.PopToRootAsync();
        enableAll();
    }
    private void disableAll()
    {
        activityController.turnOn();
        salva.IsEnabled = false;

    }
    private void enableAll()
    {
        activityController.turnOff();
        salva.IsEnabled = true;
    }
    private async Task<bool> OnValidateClicked()
    {
        if (double.TryParse(Latitudine.Text.Replace(".",","), out double latitude) &&
            double.TryParse(Longitudine.Text.Replace(".", ","), out double longitude))
        {
            if (IsLatitudeValid(latitude) && IsLongitudeValid(longitude))
            {
                //// Coordinates are valid, show on map
                //map.Pins.Clear();
                //map.Pins.Add(new Pin
                //{
                //    Label = "Entered Location",
                //    Position = new Position(latitude, longitude)
                //});

                //map.MoveToRegion(MapSpan.FromCenterAndRadius(
                //    new Position(latitude, longitude), Distance.FromKilometers(1)));

                var placemarks = await Geocoding.GetPlacemarksAsync(latitude, longitude);
                var placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    string str = "Vuoi configurare per:\n";
                    if (placemark.CountryName != null)
                    {
                        str = str + "\nNazione: " + placemark.CountryName;
                    }
                    if (placemark.AdminArea != null)
                    {
                        str = str + "\nRegione: " + placemark.AdminArea;
                    }
                    if (placemark.Locality != null)
                    {
                        str = str + "\nCitta: " + placemark.Locality;
                    }
                    if (placemark.Thoroughfare != null)
                    {
                        str = str + "\nVicino: " + placemark.Thoroughfare;
                    }

                    var res = await App.Current.MainPage.DisplayAlert("Attenzione", str, "Si", "No");
                        if (res == true)
                            return true;
                        else return false;
                    }   
                else
                {
                    await DisplayAlert("Attenzione", "Non possiamo determinare la citta", "Riprova");
                    return false;
                }
            }
            else
            {
                await DisplayAlert("Attenzione", "Non possiamo determinare la citta", "Riprova");
                return false;
            }
        }
        else
        {
            await DisplayAlert("Attenzione", "Non possiamo determinare la citta", "Riprova");
            return false;
       }
    }

    private bool IsLatitudeValid(double latitude)
    {
        return latitude >= -90 && latitude <= 90;
    }

    private bool IsLongitudeValid(double longitude)
    {
        return longitude >= -180 && longitude <= 180;
    }
}



