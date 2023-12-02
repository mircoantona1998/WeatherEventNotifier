using AppWeatherEventNotifier.Models;
using System.Collections.ObjectModel;


namespace AppWeatherEventNotifier.ViewModels;

public class HomeViewModel : BaseViewModel
{
    private ObservableCollection<Tip> _Intentions_tips { get; set; } = new ObservableCollection<Tip>();
    public ObservableCollection<Brush> CustomBrushesShared { get; set; }
    public ObservableCollection<Brush> CustomBrushesCons { get; set; }
    public ObservableCollection<Brush> CustomBrushesProd { get; set; }
    public ObservableCollection<Brush> CustomBrushesSharedDay { get; set; }
    public ObservableCollection<Brush> CustomBrushesConsDay { get; set; }
    public ObservableCollection<Brush> CustomBrushesProdDay { get; set; }
    public List<DateTime> SpecialDatesList { get; set; }
    public HomeViewModel()
    {
  
    }

    public ObservableCollection<Tip> Intentions_tips
    {
        get { return _Intentions_tips; }
        set
        {
            _Intentions_tips = value;

            OnPropertyChanged();
        }
    }

}