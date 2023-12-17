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
    }

    protected override async void OnAppearing()
    {
        disableAll();
        listMetric = await MetricController.get_metrics();
        if (listMetric != null)
        {
            metrica.ItemsSource = listMetric;
            metrica.ItemDisplayBinding = new Binding("Field");
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
        Frequency foundFrequency = frequencyList.FirstOrDefault(f => f.FrequencyName == frequencySelected.FrequencyName);
        int? IdFrequency = foundFrequency.Id;
        float Longitude = Convert.ToInt64(longitudine.Text.ToString());
        float Latitude = Convert.ToInt64(latitudine.Text.ToString());
        Metric foundMetric = listMetric.FirstOrDefault(f => f.Field == metricSelected.Field);
        int? IdMetric = foundMetric.Id;
        string Symbol = Simbolo.SelectedItem.ToString();
        float Value = Convert.ToInt64(Valore.Text.ToString());
        var resp = await ConfigurationController.patch_configuration(Globals.ConfigurationSelected.Id,Longitude, Latitude, IdMetric, IdFrequency, Symbol, Value);
        if (resp == true)
        {
            await DisplayAlert("Successo", "Configurazione modificata correttamente", "Ok");
            await Refresh.refreshInfoUser();
        }
        else
        {
            await DisplayAlert("Error", "Errore nella modifica della configurazione", "Ok");
        }
        await Navigation.PopAsync();
        await Navigation.PopAsync();
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
}



