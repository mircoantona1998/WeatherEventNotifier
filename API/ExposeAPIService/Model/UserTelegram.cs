using Confluent.Kafka;
using ExposeAPI.Interface;
using ExposeAPI.Kafka;

namespace ExposeAPI.Model
{
    public class UserTelegram : IUserTelegram
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string ChatId { get; set; }
        public bool? IsActive { get; set; }

    }

}
