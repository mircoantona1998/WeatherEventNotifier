using Confluent.Kafka;
using ExposeAPI.Interface;
using ExposeAPI.Kafka;

namespace ExposeAPI.Model
{
    public class TelegramSent : ITelegramSent
    {
        public int Id { get; set; }
        public string IdChat { get; set; }
        public string Testo { get; set; }
        public bool? Allegati { get; set; }
        public DateTime? DateCreate { get; set; }
        public bool? WasSent { get; set; }
        public string Result { get; set; }

    }

}
