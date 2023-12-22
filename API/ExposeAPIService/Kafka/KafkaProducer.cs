using Confluent.Kafka;
using ExposeAPI.DB;
using ExposeAPI.Utils;
using System.Text;
using Userdata.Models;
using Userdata.ViewModels;

namespace ExposeAPI.Kafka
{
    public class KafkaProducer
    {
        private readonly MessageSentRepository messSentRepo;
        public KafkaProducer()
        {
            messSentRepo = new MessageSentRepository(config.confdb);
        }
        public async Task<DeliveryResult<Null, string>> ProduceRequest<T>(Object message,MessageType type,MessageTag tag,string topic)
        {
            string mess = Json.ConvertObjectToJson(message);
            using var producer = new ProducerBuilder<Null, string>(Kafka.producerConfig).Build();
            var headers = new Headers {
                { "Creator", Encoding.UTF8.GetBytes(config.configuration["groupID"]) },
                { "IdOffsetResponse", Encoding.UTF8.GetBytes("-1") },
                { "Type", Encoding.UTF8.GetBytes(EnumUtils.EnumToString(type)) },
                { "Tag", Encoding.UTF8.GetBytes(EnumUtils.EnumToString(tag)) },
                { "Code", Encoding.UTF8.GetBytes(EnumUtils.EnumToString(MessageCode.Ok)) },
            };
            var result = producer.ProduceAsync(topic, new Message<Null, string> { Value = mess, Headers = headers }).Result;
            await saveMessage(result,headers);
            Console.WriteLine($"Produced message {result.Message.Value} on {result.TopicPartitionOffset}");
            return result;
        }
        public async Task saveMessage(DeliveryResult<Null,string> result,Headers? headers)
        {
            MessageSentDTO messag = new MessageSentDTO();
            messag.Timestamp = DateTime.UtcNow;
            messag.Message = result.Message.Value;
            messag.Offset = ((int)result.Offset.Value);
            var senderIdHeader = headers.GetLastBytes("Creator");
            if (senderIdHeader != null)
            {
                messag.Creator = Encoding.UTF8.GetString(senderIdHeader);
            }
            var IdoffsetResponseB = headers.GetLastBytes("IdOffsetResponse");
            if (IdoffsetResponseB != null)
            {
                string IdoffsetResponseString = Encoding.UTF8.GetString(IdoffsetResponseB);
                messag.IdOffsetResponse = int.Parse(IdoffsetResponseString);
            }
            var TypeB = headers.GetLastBytes("Type");
            if (TypeB != null)
            {
                messag.Type = Encoding.UTF8.GetString(TypeB);
            }
            var TagB = headers.GetLastBytes("Tag");
            if (TagB != null)
            {
                messag.TagMessage = Encoding.UTF8.GetString(TagB);
            }
            var CodB = headers.GetLastBytes("Code");
            if (CodB != null)
            {
                messag.Code = Encoding.UTF8.GetString(CodB);
            }
            messag.Topic = result.Topic;
            messag.Partition= result.Partition; 
            await messSentRepo.Create(messag);
        }
    }
}
