using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppWeatherEventNotifier.Models
{
    public class InfoUser
    {
        [JsonProperty("idUtente")]
        public int IdUtente { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("telefono")]
        public string Telefono { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("codiceSblocco")]
        public string CodiceSblocco { get; set; }

        [JsonProperty("creazione")]
        public DateTime Creazione { get; set; }

        [JsonProperty("tempPassword")]
        public bool TempPassword { get; set; }

        [JsonProperty("mobile")]
        public int Mobile { get; set; }

        [JsonProperty("privacy")]
        public bool Privacy { get; set; }
    }
}
