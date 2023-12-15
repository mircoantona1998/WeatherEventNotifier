using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppWeatherEventNotifier.ViewModels;

    public class BaseViewModel : INotifyPropertyChanged
    {
        string _usernameEntry;
        string _passwordEntry;
        public string PasswordEntry
        {
            set
            {
                SetProperty(ref _passwordEntry, value);

            }

            get { return _passwordEntry; }
        }
        public string UsernameEntry
        {
            set
            {
                SetProperty(ref _usernameEntry, value);

            }

            get { return _usernameEntry; }
        }
            protected bool SetProperty<T>(ref T backingStore, T value,
                    [CallerMemberName] string propertyName = "",
                    Action onChanged = null)
                {
                    if (EqualityComparer<T>.Default.Equals(backingStore, value))
                        return false;

                    backingStore = value;
                    onChanged?.Invoke();
                    OnPropertyChanged(propertyName);
                    return true;
                }

                #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

