

using AppWeatherEventNotifier.Helper;
using AppWeatherEventNotifier.Services.Events;

namespace AppWeatherEventNotifier.Views.HeaderProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderContentView : ContentView
    {
        public HeaderContentView()
        {
            InitializeComponent();        
            foto.Source = "amministratore.png";
            nome.Text = AppWeatherEventNotifier.Helper.TodoItemDatabase.Instance.UsernameEntry;          
        }
      
   

        
        private async void refresh_Clicked(object sender, EventArgs e)
        {
          
        }
        private async void refreshEventCalled()
        {

        }
    }
}