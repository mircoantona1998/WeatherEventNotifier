using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AppWeatherEventNotifier.Models
{
   public class RefreshTokenResponse
    {
        [JsonPropertyName("token")]
        public Dictionary<string, string> Token { get; set; }

    }
}
