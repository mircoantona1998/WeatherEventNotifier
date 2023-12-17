using AppWeatherEventNotifier.Models;
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

        List<Metric> listMetric = await MetricController.get_metrics();
        if (listMetric != null)
        {
            metrica.ItemsSource = listMetric;
            metrica.ItemDisplayBinding = new Binding("Field");
            metrica.SelectedIndexChanged += OnPickerSelectedIndexChangedMetric;
        }
        frequenza.ItemsSource = frequencyList;
        frequenza.ItemDisplayBinding = new Binding("FrequencyName");
        frequenza.SelectedIndexChanged += OnPickerSelectedIndexChangedFrequency;

        //if (Globals.ConfigurationSelected.IdConfiguration != null)
        //    idLabel.Text = Globals.ConfigurationSelected.IdConfiguration.ToString();
        //if (Globals.ConfigurationSelected.Title != null)
        //    titoloLabel.Text = Globals.ConfigurationSelected.Title.ToString();
        //if (Globals.ConfigurationSelected.Note != null)
        //    descrizioneEntry.Text = Globals.ConfigurationSelected.Note.ToString();
        //if (Globals.ConfigurationSelected.Date != null)
        //    data.Text = Globals.ConfigurationSelected.Date.ToString();
        //if (Globals.ConfigurationSelected.Source != null)
        //    sorgente.Text = Globals.ConfigurationSelected.Source.ToString();
        //if (Globals.ConfigurationSelected.GroupingTAG != null)
        //    gruppo.Text = Globals.ConfigurationSelected.GroupingTAG.ToString();
        //if (Globals.ConfigurationSelected.IdAlarm != null)
        //    codice_allarme.Text = Globals.ConfigurationSelected.IdAlarm.ToString();
        //if (Globals.ConfigurationSelected.Priority != null)
        //    priorit‡Label.Text = Globals.ConfigurationSelected.Priority.ToString();
        //if (Globals.ConfigurationSelected.Plant != null)
        //    impiantoLabel.Text = Globals.ConfigurationSelected.Plant.ToString();
        //if (Globals.ConfigurationSelected.IdStatusConfiguration != null)
        //    if (Globals.ConfigurationSelected.IdStatusConfiguration == 1)
        //        stato.Text = "Attivo";
        //    else
        //        stato.Text = "Completato";

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
    /*
     * Save Configuration
     */
    private async void SaveConfigurationClicked(object sender, EventArgs e)
    {
         disableAll();

        //Models.ModifiedConfiguration Configuration = new Models.ModifiedConfiguration
        //{
        //    IdConfiguration = Globals.ConfigurationSelected.IdConfiguration,
        //    DateUpdate = DateTime.Now,
        //    Note = descrizioneEntry.Text,
        //    IdStatusConfiguration = 1,
        //    Priority = priorit‡Label.Text.ToString(),
        //    IdPlant = Globals.ConfigurationSelected.IdPlant
        //};

        //var resp = await _model.OnSaveModifiedConfiguration(Configuration);

        //if (resp.Result == true)
        //{
        //    await DisplayAlert("Success", "Configuration modificato correttamente", "Chiudi");
        //    await Services.RestController.Refresh.RefreshHomePageData.RefreshConfigurations();
        //    Globals.ConfigurationSelected.Note = descrizioneEntry.Text;
        //}
        //else
        //{
        //    await DisplayAlert("Error", "Errore nella modifica del Configuration", "Chiudi");
        //}
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



