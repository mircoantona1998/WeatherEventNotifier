using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AppWeatherEventNotifier.Models
{
   public class UserTelegram
    {
        public int Id { get; set; }
        public string ChatId { get; set; }
        public bool? IsActive { get; set; }
        public int IdUser { get; set; }
       

    }
}
