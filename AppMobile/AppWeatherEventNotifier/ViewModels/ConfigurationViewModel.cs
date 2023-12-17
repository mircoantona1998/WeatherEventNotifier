using AppWeatherEventNotifier.Models;
using System.Collections.ObjectModel;


namespace AppWeatherEventNotifier.ViewModels;

public class ConfigurationViewModel : BaseViewModel
{
    public ObservableCollection<Configuration> _Intentions_configurations { get; set; } = new ObservableCollection<Configuration>();
    public ObservableCollection<Metric> _Intentions_metrics { get; set; } = new ObservableCollection<Metric>();
    public ObservableCollection<Frequency> _Intentions_frequencys { get; set; } = new ObservableCollection<Frequency>();

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
    public ObservableCollection<Frequency> Intentions_frequencys
    {
        get { return _Intentions_frequencys; }
        set
        {
            _Intentions_frequencys = value;

            OnPropertyChanged();
        }
    }
    public ObservableCollection<Metric> Intentions_metrics
    {
        get { return _Intentions_metrics; }
        set
        {
            _Intentions_metrics = value;

            OnPropertyChanged();
        }
    }

}