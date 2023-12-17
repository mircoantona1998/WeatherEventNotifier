namespace ExposeAPI.Kafka
{
    public class KafkaMessage<T>
    {
        public int IdOffsetResponse{ get; set; }
        public string? Type { get; set; }
        public string? Tag { get; set; }
        public bool? Code { get; set; }
        public T? Data { get; set; } 
        
    }

}
