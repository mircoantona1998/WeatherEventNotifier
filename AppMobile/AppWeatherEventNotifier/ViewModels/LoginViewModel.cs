
using AppWeatherEventNotifier.Helper;
using System.Windows.Input;

namespace AppWeatherEventNotifier.ViewModels;


public class LoginViewModel : BaseViewModel
{
    public LoginViewModel()
    { }
         private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    private string _buttonText = "Click me";
    public string ButtonText
    {
        get => _buttonText;
        set => SetProperty(ref _buttonText, value);
    }

    private int _count;

    #region Commands
    public ICommand ButtonClickCommand => new Command(async () =>
    {
        IsBusy = true;
        _count++;
        await Task.Delay(500);
        ButtonText = $"Clicked {_count} time";
        IsBusy = false;
    });
    #endregion
    
    public async Task<bool> OnLoginClicked()
    {

        if (UsernameEntry == "" || PasswordEntry == "" || UsernameEntry == null || PasswordEntry == null)
        {
            return false;
        }
        return await Services.RestController.LoginController.login_request(UsernameEntry, PasswordEntry);

   
    }
    
}