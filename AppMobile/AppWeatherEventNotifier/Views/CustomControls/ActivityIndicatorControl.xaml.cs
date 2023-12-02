using System.Windows.Input;

namespace AppWeatherEventNotifier.Views.CustomControls;

public partial class ActivityIndicatorControl : Frame
{
    public ActivityIndicatorControl()
    {
        InitializeComponent();
    }
    public void turnOn()
    {
        loading.IsVisible = true;
        barLoading.IsRunning = true;
    }
    public void turnOff()
    {
        barLoading.IsRunning = false;
        loading.IsVisible = false;
    }
   
}