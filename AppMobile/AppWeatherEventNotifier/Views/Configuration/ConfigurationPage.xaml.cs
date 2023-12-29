using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.ViewModels;
using AppWeatherEventNotifier.Services.RestController;
using AppWeatherEventNotifier.Services;

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
        if (Globals.ConfigurationSelected.Longitude != null)
            Longitudine.Text = Globals.ConfigurationSelected.Longitude.ToString();
        if (Globals.ConfigurationSelected.Latitude != null)
            Latitudine.Text = Globals.ConfigurationSelected.Latitude.ToString();
        if (Globals.ConfigurationSelected.DateTimeCreate != null)
            Data_creazione.Text = Globals.ConfigurationSelected.DateTimeCreate.Value.ToLocalTime().ToString();
        if (Globals.ConfigurationSelected.DateTimeActivation != null)
            Data_attivazione.Text = Globals.ConfigurationSelected.DateTimeActivation.Value.ToLocalTime().ToString();
        if (Globals.ConfigurationSelected.Description != null)
            Metrica.Text = Globals.ConfigurationSelected.Description.ToString();
        if (Globals.ConfigurationSelected.FrequencyName != null)
            Frequenza.Text = Globals.ConfigurationSelected.FrequencyName.ToString();
        if (Globals.ConfigurationSelected.IsActive != null && Globals.ConfigurationSelected.IsActive == true)
            Attiva.IsToggled = true;
        else Attiva.IsToggled = false;
        if (Globals.ConfigurationSelected.Symbol != null)
            Simbolo.Text = Globals.ConfigurationSelected.Symbol;
        if (Globals.ConfigurationSelected.Value != null)
            Valore.Text = Globals.ConfigurationSelected.Value.ToString();
        if (Globals.ConfigurationSelected.ValueUnit != null)
            ValueUnit.Text = Globals.ConfigurationSelected.ValueUnit.ToString();
    }

    private async void EditConfigurationClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditConfiguration());
    }
    private async void DeleteConfigurationClicked(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet("Sei sicuro di voler eliminare la configurazione?", "Chiudi", null, "Si", "No");
        if (action == "Si")
        {
            disableAll();
            var resp = await ConfigurationController.delete_configuration(Globals.ConfigurationSelected.Id);
            if (resp == true)
            {
                await ConfigurationController.get_configurations();
                await DisplayAlert("Success", "Configurazione eliminata correttamente", "Chiudi");
                await Refresh.refreshInfoUser();
                await Navigation.PopToRootAsync();
            }
            else
            {
                await DisplayAlert("Error", "Errore nell'eliminazione della configurazione", "Chiudi");
            }
            enableAll();
        }
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



