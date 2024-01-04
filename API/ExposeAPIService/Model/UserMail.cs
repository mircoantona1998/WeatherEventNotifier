using Confluent.Kafka;
using ExposeAPI.Interface;
using ExposeAPI.Kafka;

namespace ExposeAPI.Model
{
    public class UserMail : IUserMail
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Mail { get; set; }
        public bool? IsActive { get; set; }
    }

}
