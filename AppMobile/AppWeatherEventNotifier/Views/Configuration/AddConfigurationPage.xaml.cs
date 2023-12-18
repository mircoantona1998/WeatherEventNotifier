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
        if(frequencySelected!=null && metricSelected != null && Longitudine.Text!="" && Latitudine.Text!="" && Simbolo.SelectedItem !=null && Valore.Text!="")
        { 
            int? IdFrequency = frequencySelected.Id;
            float Longitude = Convert.ToInt64(Longitudine.Text.ToString());
            float Latitude = Convert.ToInt64(Latitudine.Text.ToString());
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
}



