using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.ViewModels;
using AppWeatherEventNotifier.Services.RestController;


namespace AppWeatherEventNotifier.Views.Configuration;

public partial class ConfigurationPage : ContentPage
{
    private ConfigurationViewModel _model;

    public ConfigurationPage()
    {
        InitializeComponent();
        BindingContext = _model = new ConfigurationViewModel();
    }

    protected override void OnAppearing()
    {
        //if (Globals.ConfigurationSelected.IdConfiguration != null)
        //    id.Text = Globals.ConfigurationSelected.IdConfiguration.ToString();
        //if (Globals.ConfigurationSelected.Title != null)
        //    titolo.Text = Globals.ConfigurationSelected.Title.ToString();
        //if (Globals.ConfigurationSelected.Plant != null)
        //    impianto.Text = Globals.ConfigurationSelected.Plant.ToString();
        //if (Globals.ConfigurationSelected.Date != null)
        //    data.Text = Globals.ConfigurationSelected.Date.ToString();
        //if (Globals.ConfigurationSelected.Source != null)
        //    sorgente.Text = Globals.ConfigurationSelected.Source.ToString();
        //if (Globals.ConfigurationSelected.IdAlarm != null)
        //    codice_allarme.Text = Globals.ConfigurationSelected.IdAlarm.ToString();
        //if (Globals.ConfigurationSelected.Note != null)
        //    descrizione.Text = Globals.ConfigurationSelected.Note.ToString();
           
        //if (Globals.ConfigurationSelected.Priority != null)
        //    priorita.Text = Globals.ConfigurationSelected.Priority.ToString();
        //if (Globals.ConfigurationSelected.GroupingTAG != null)
        //    gruppo.Text = Globals.ConfigurationSelected.GroupingTAG.ToString();
        //if (Globals.ConfigurationSelected.IdStatusConfiguration != null)
        //{
        //    if (Globals.ConfigurationSelected.IdStatusConfiguration == 1)
        //        stato.Text = "Attivo";
        //    else
        //    {
        //        stato.Text = "Completato";
        //        if (Globals.ConfigurationSelected.DateUpdate != null)
        //            dateUpdateValue.Text = Globals.ConfigurationSelected.DateUpdate.ToString();
        //        if (Globals.ConfigurationSelected.IdOwner != null)
        //            ownerValue.Text = Globals.ConfigurationSelected.IdOwner.ToString();
        //        if (Globals.ConfigurationSelected.NoteRisoluzione != null)
        //            noteEntry.Text = Globals.ConfigurationSelected.NoteRisoluzione;
        //        loadConfigurationCompletedlayout();
        //    }
        //}
    }

    /*
     * Edit selected Configuration
     */
    private async void EditConfigurationClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditConfiguration());
    }

    /*
     * Show Resolve Selected Configuration layout
     */
    private void ShowResolveConfigurationClicked(object sender, EventArgs e)
    {
        disableAll();
        noteLabel.IsEnabled = true;
        noteLabel.IsVisible = true;
        noteEntry.IsEnabled = true;
        noteEntry.IsVisible = true;
        noteEntryFrame.IsVisible = true;
        editConfiguration.IsVisible = false;   
        activityController.turnOff();
    }

   
    private void GoBackClicked(object sender, EventArgs e)
    {
        activityController.turnOn();

        noteLabel.IsEnabled = false;
        noteLabel.IsVisible = false;
        noteEntry.IsEnabled = false;
        noteEntry.IsVisible = false;
        noteEntryFrame.IsVisible = false;  
        editConfiguration.IsVisible = true;
        enableAll();
    }

    /*
     * Annul selected Configuration
     */
    private async void DeleteConfigurationClicked(object sender, EventArgs e)
    {
        //string action = await DisplayActionSheet("Sei sicuro di voler annullare il Configuration?", "Chiudi", null, "Si", "No");
        //if (action == "Si") 
        //{
        //    disableAll();
        //    var resp = await _model.OnAnnulledConfigurationClicked(Globals.ConfigurationSelected.IdConfiguration);
        //    if (resp.Result == true)
        //    {
        //        await Services.RestController.Refresh.RefreshHomePageData.RefreshConfigurations();
        //        await DisplayAlert("Success", "Configuration annullato correttamente", "Chiudi");
        //        await Navigation.PopAsync();
        //    }
        //    else
        //    {
        //        await DisplayAlert("Error", "Errore nell'annullamento del Configuration", "Chiudi");
        //    }
        //    enableAll();
        //}
    }

    /*
     * Disable all button
     */
    private void loadConfigurationCompletedlayout()
    {
        info.Title = "Visualizza Configuration";

        // Disable buttons
        editConfiguration.IsEnabled = false;
        editConfiguration.IsVisible = false;
        noteEntry.IsEnabled = false;
        noteEntryFrame.IsVisible =true;     
        
        // Show Owner and date update
        dateUpdateName.IsVisible = true;
        dateUpdateValue.IsVisible = true;
        ownerName.IsVisible = true;
        ownerValue.IsVisible = true;
        noteLabel.IsVisible = true;
        noteEntry.IsVisible = true;
        noteEntryFrame.IsVisible = true;

        dateUpdateName.Text = "Data chiusura";
    }

    private void disableAll()
    {
        activityController.turnOn();
        deleteConfiguration.IsEnabled = false;     
        editConfiguration.IsEnabled = false;
    }
    private void enableAll()
    {
        activityController.turnOff();
        deleteConfiguration.IsEnabled = true;       
        editConfiguration.IsEnabled = true;
    }
}



