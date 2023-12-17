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
            Frequency foundFrequency = frequencyList.FirstOrDefault(f => f.FrequencyName == frequencySelected.FrequencyName);
            int? IdFrequency = foundFrequency.Id;
            float Longitude = Convert.ToInt64(Longitudine.Text.ToString());
            float Latitude = Convert.ToInt64(Latitudine.Text.ToString());
            Metric foundMetric = listMetric.FirstOrDefault(f => f.Field==metricSelected.Field);
            int? IdMetric = foundMetric.Id;
            string Symbol = Simbolo.SelectedItem.ToString();
            float Value = Convert.ToInt64(Valore.Text.ToString());

            var resp = await ConfigurationController.add_configuration( Longitude, Latitude, IdMetric, IdFrequency, Symbol, Value);
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
        Navigation.RemovePage(this);
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



