
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Java.Util.Logging;
using Microsoft.Maui.Controls;

namespace AppWeatherEventNotifier;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Nosensor, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        Window.SetStatusBarColor(Android.Graphics.Color.Rgb(41, 80, 163));
        Window.SetNavigationBarColor(Android.Graphics.Color.Rgb(41, 80, 163));
        base.OnCreate(savedInstanceState);
    }
}
