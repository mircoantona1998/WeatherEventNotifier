
using AppWeatherEventNotifier.Models;
using AppWeatherEventNotifier.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace AppWeatherEventNotifier.Helper
{
    public class TodoItemDatabase : BaseViewModel
    {
        private static TodoItemDatabase instance = null;
        private static readonly object padlock = new object();
        public static TodoItemDatabase Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new TodoItemDatabase();
                    }
                    return instance;
                }
            }
        }
        public string Token { get; set; }
        public string Refresh_Token { get; set; }
        private bool isLoggedIn;
        public bool IsLoggedIn
        {
            get => isLoggedIn;
            set => SetProperty(ref isLoggedIn, value);
        }
        public string UserId { get; set; }
       
        public  List<Tip> TipsUser { get; set; }  //lista notifiche dell'utente  
        public List<Language> ListLanguages { get; set; }
        private Language selLanguage;
        public Language SelectedLanguage
        {
            get => selLanguage;
            set
            {
                string img = null;
                switch (value.Coding)
                {
                    case "it-IT":
                        img = "ITALIANO";
                        break;
                    case "en-GB":
                        img = "INGLESE";
                        break;
                    case "es-ES":
                        img = "spagnolo";
                        break;
                    case "pt-PT":
                        img = "portoghese";
                        break;
                    case "ar-AE":
                        img = "emirati";
                        break;
                    case "ro-RO":
                        img = "romano";
                        break;
                    default:
                        break;
                }
                if (img != null)
                {
                    string source = $"{img}.png";
                    value.ImageSource = ImageResourceExtension.SetImage(source);
                }
                selLanguage = value;
            }
        }
       
    }

}
