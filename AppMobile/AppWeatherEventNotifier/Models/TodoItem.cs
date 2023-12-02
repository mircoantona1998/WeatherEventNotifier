using System.Collections.Generic;
using SQLite;

namespace AppWeatherEventNotifier.Models
{
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }
    }
}