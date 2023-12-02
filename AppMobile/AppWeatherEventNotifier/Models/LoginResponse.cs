using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AppWeatherEventNotifier.Models
{
   public class LoginResponse
    {  public int httpcode { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public bool changeMe { get; set; }       
        public int ErrorCode { get; set; }

        [JsonPropertyName("token")]
        public Dictionary<string,string> Token { get; set; }

    }
}
