using Newtonsoft.Json;
using AppWeatherEventNotifier.ViewModels;

using AppWeatherEventNotifier.Services;
using AppWeatherEventNotifier.Helper;

namespace AppWeatherEventNotifier.Models
{
    public class Language : BaseViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("desc")]
        public string Descrizione { get; set; }
        string coding;

        [JsonProperty("coding")]
        public string Coding
        {
            get => coding;
            set
            {
                SetProperty(ref coding, value);
                string img = null;
                switch (value)
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
                    case "el-GR":
                        img = "greco";
                        break;
                    default:
                        break;
                }
                if (img != null)
                {
                    string source = $"{img}.png";
                    ImageSource = ImageResourceExtension.SetImage(source);
                }
            }
        }

        [JsonProperty("texts")]
        public object Texts { get; set; }
        [JsonIgnore]
        public ImageSource ImageSource { get; set; }

        //variabile per icona lingua preferita
        private string languageIcon = IconFont.HeartOutline;
        public string LanguageIcon
        {
            get => languageIcon;
            set => SetProperty(ref languageIcon, value);
        }
    }


}
