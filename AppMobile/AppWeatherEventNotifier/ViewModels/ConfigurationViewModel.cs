using AppWeatherEventNotifier.Models;
using System.Collections.ObjectModel;


namespace AppWeatherEventNotifier.ViewModels;

public class ConfigurationViewModel : BaseViewModel
{
    public ObservableCollection<Configuration> _Intentions_configurations { get; set; } = new ObservableCollection<Configuration>();
    public ConfigurationViewModel()
    {
  
    }

    public ObservableCollection<Configuration> Intentions_configurations
    {
        get { return _Intentions_configurations; }
        set
        {
            _Intentions_configurations = value;

            OnPropertyChanged();
        }
    }

}