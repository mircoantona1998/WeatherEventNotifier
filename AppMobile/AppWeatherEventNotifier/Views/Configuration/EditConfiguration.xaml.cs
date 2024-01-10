using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Models;
using AppWeatherEventNotifier.Services;
using AppWeatherEventNotifier.Services.RestController;
using AppWeatherEventNotifier.ViewModels;

namespace AppWeatherEventNotifier.Views.Configuration;

public partial class EditConfiguration : ContentPage
{
    private ConfigurationViewModel _model;
    private List<Metric> listMetric;
    private List<Frequency> frequencyList;
    private Frequency frequencySelected;
    private Metric metricSelected;

    public EditConfiguration()
    {
        InitializeComponent();
        BindingContext = _model = new ConfigurationViewModel();
        data_attivazione.MinimumDate = DateTime.Today;
    }

    protected override async void OnAppearing()
    {
        disableAll();
        listMetric = await MetricController.get_metrics();
        if (listMetric != null)
        {
            metrica.ItemsSource = listMetric;
            metrica.ItemDisplayBinding = new Binding("Description");
        }
        frequencyList = await FrequencyController.get_frequencys();
        if (frequencyList != null)
        {
            frequenza.ItemsSource = frequencyList;
            frequenza.ItemDisplayBinding = new Binding("FrequencyName");
        }
        if (Globals.ConfigurationSelected.IdMetric != null)
        {
            metricSelected = listMetric.FirstOrDefault(metric => metric.Id == Globals.ConfigurationSelected.IdMetric);
            if (metricSelected != null)
            {
                metrica.SelectedItem = metricSelected;
            }
        }
        if (Globals.ConfigurationSelected.IdFrequency != null)
        {
            frequencySelected = frequencyList.FirstOrDefault(Frequency => Frequency.Id == Globals.ConfigurationSelected.IdFrequency);
            if (frequencySelected != null)
            {
                frequenza.SelectedItem = frequencySelected;
            }
        }
        if (Globals.ConfigurationSelected.NameConfiguration != null)
            NameConfiguration.Text = Globals.ConfigurationSelected.NameConfiguration.ToString();
        if (Globals.ConfigurationSelected.Longitude != null)
            longitudine.Text = Globals.ConfigurationSelected.Longitude.ToString();
        if (Globals.ConfigurationSelected.Latitude != null)
            latitudine.Text = Globals.ConfigurationSelected.Latitude.ToString();
        if (Globals.ConfigurationSelected.DateTimeActivation!= null)
            data_attivazione.Date = Globals.ConfigurationSelected.DateTimeActivation.Value.Date.ToLocalTime();
            time_attivazione.Time = Globals.ConfigurationSelected.DateTimeActivation.Value.ToLocalTime().TimeOfDay;
        if (Globals.ConfigurationSelected.IsActive != null && Globals.ConfigurationSelected.IsActive == true)
            isActive.IsToggled = true;
        else isActive.IsToggled =false;
        if (Globals.ConfigurationSelected.Symbol != null)
            Simbolo.SelectedItem = Globals.ConfigurationSelected.Symbol.ToString();
        if (Globals.ConfigurationSelected.Value != null)
            Valore.Text = Globals.ConfigurationSelected.Value.ToString();
        if (Globals.ConfigurationSelected.ValueUnit != null)
            ValueUnit.Text = Globals.ConfigurationSelected.ValueUnit.ToString();
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
        var res = await OnValidateClicked();
        if (res == false) return;
        if (NameConfiguration.Text!="" && frequencySelected != null && metricSelected != null && longitudine.Text.Replace(".", ",") != "" && latitudine.Text.Replace(".", ",") != "" && Simbolo.SelectedItem != null && Valore.Text != "")
        {
            int? IdFrequency = frequencySelected.Id;
            float Longitude;
            if (float.TryParse(longitudine.Text.Replace(".", ","), out Longitude))
            {
            }
            else
            {
                await DisplayAlert("Error", "Longitudine non valida", "Ok");
                return;
            }
            float Latitude;
            if (float.TryParse(latitudine.Text.Replace(".", ","), out Latitude))
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
            bool? IsActive = isActive.IsToggled;


            var resp = await ConfigurationController.patch_configuration(NameConfiguration.Text,Globals.ConfigurationSelected.Id,Longitude, Latitude, IdMetric, IdFrequency, Symbol, Value, dateTimeAttivazione, IsActive);
            if (resp == true)
            {
                await DisplayAlert("Successo", "Configurazione modificata correttamente", "Ok");
                await Refresh.refreshInfoUser();
            }
            else
            {
                await DisplayAlert("Error", "Errore nella modifica della configurazione", "Ok");
            }
        await Navigation.PopToRootAsync();
        }
        else
        {
            await DisplayAlert("Attenzione", "Devi compilare tutti i campi", "Ok");
            enableAll();
            return;
        }
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
        if (double.TryParse(latitudine.Text.Replace(".", ","), out double latitude) &&
            double.TryParse(longitudine.Text.Replace(".", ","), out double longitude))
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



