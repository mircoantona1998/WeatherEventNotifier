namespace UserdataService.Kafka
{
    public class KafkaMessage<T>
    {
        public int IdOffsetResponse{ get; set; }
        public string? Type { get; set; }
        public string? Tag { get; set; }
        public T? Data { get; set; } 
        
    }

}
