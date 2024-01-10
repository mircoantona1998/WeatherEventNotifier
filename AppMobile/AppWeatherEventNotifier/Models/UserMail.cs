using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AppWeatherEventNotifier.Models
{
   public class UserMail
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public bool? IsActive { get; set; }
        public int IdUser { get; set; }

    }
}
