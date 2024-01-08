using Confluent.Kafka;
using ExposeAPI.Interface;
using ExposeAPI.Kafka;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExposeAPI.Model
{
    public class Notifier : INotifier
    {
        public int Id { get; set; }

        public int? IdUser { get; set; }

        public int? IdSchedule { get; set; }

        public string Message { get; set; }

        public DateTime? DateTimeCreate { get; set; }

        public int? IdConfiguration { get; set; }

        public float? ValueWeather { get; set; }


    }
}
