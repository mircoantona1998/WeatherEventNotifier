using Confluent.Kafka;
using ExposeAPI.DB;
using ExposeAPI.Utils;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Userdata.Models;
using Userdata.ViewModels;

namespace ExposeAPI.Kafka
{
    public class KafkaProducer
    {
        private readonly string topic_request;
        private readonly MessageSentRepository messSentRepo;
        public KafkaProducer( string topic_request)
        {
            messSentRepo = new MessageSentRepository(config.confdb);
            this.topic_request = topic_request;
        }
        public async Task<int> ProduceRequest<T>(Object message,MessageType type,MessageTag tag)
        {
            string mess=Json.PackagingMessage(message, type,tag,-1);
            using var producer = new ProducerBuilder<Null, string>(Kafka.producerConfig).Build();
            var result = await producer.ProduceAsync(topic_request, new Message<Null, string> { Value = (string)mess });
            KafkaMessage<T> obj = Json.ConvertJsonToObject<KafkaMessage<T>>(result.Message.Value);
            await saveMessage(result,obj);
            Console.WriteLine($"Produced message {result.Message.Value} to {result.TopicPartitionOffset}");
            return (int)result.Offset;
        }
        public async Task saveMessage<T>(DeliveryResult<Null,string> result, KafkaMessage<T> kf)
        {
            MessageSentDTO messag = new MessageSentDTO();
            messag.Timestamp = DateTime.UtcNow;
            messag.Message = result.Message.Value;
            messag.Offset = ((int)result.Offset.Value);
            messag.TagMessage = kf.Tag;
            messag.Type = kf.Type;
            if (kf.Type == MessageType.Response.ToString())
                messag.IdOffsetResponse = kf.IdOffsetResponse;
            messag.Topic = topic_request;
            await messSentRepo.Create(messag);
        }
    }
}
