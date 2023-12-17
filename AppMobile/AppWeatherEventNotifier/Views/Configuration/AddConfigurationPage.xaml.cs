using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.ViewModels;

namespace AppWeatherEventNotifier.Views.Configuration;

public partial class AddConfigurationPage : ContentPage
{
    private ConfigurationViewModel _model;
    //private List<Implant> implantsList;
    //private List<Status> statusList;
    //private Status statusSelected;
    //private Implant implantSelected;

    public AddConfigurationPage()
    {
        InitializeComponent();
        BindingContext = _model = new ConfigurationViewModel();
    }

    protected override async void OnAppearing()
    {
        disableAll();

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
        //    prioritÓLabel.Text = Globals.ConfigurationSelected.Priority.ToString();
        //if (Globals.ConfigurationSelected.Plant != null)
        //    impiantoLabel.Text = Globals.ConfigurationSelected.Plant.ToString();
        //if (Globals.ConfigurationSelected.IdStatusConfiguration != null)
        //    if (Globals.ConfigurationSelected.IdStatusConfiguration == 1)
        //        stato.Text = "Attivo";
        //    else
        //        stato.Text = "Completato";

        enableAll();
    }


    // Set implant selected
    private void OnPickerSelectedIndexChangedImplant(object sender, EventArgs e)
    {
        // Handle the selected item change event here
      //  implantSelected = (Implant)((Picker)sender).SelectedItem;
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
        //    Priority = prioritÓLabel.Text.ToString(),
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



