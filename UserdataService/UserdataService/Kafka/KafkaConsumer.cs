using Confluent.Kafka;
using UserdataService.DB;
using UserdataService.Utils;
using Userdata.Models;
using Userdata.ViewModels;

namespace UserdataService.Kafka
{
    public class KafkaConsumer
    {
        private readonly string bootstrapServers;
        private readonly string groupId;
        private readonly string topic_response;
        private readonly MessageReceivedRepository messRepo;

        public KafkaConsumer(string bootstrapServers, string groupId,string topic_response)
        {
            messRepo = new MessageReceivedRepository(config.confdb);
            this.bootstrapServers = bootstrapServers;
            this.groupId = groupId;
            this.topic_response = topic_response;
        }

        public async Task<T> ConsumeResponse<T>(int offset)
        {
            T response = default(T);
            var config = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(topic_response);
            while (true)
            {
                try
                {
                    var result = consumer.Consume(TimeSpan.FromMilliseconds(100));
                    if (result != null)
                    {
                        var messLast = await messRepo.GetLast();
                        if (messLast == null || ((int)result.Offset.Value) > messLast.Offset)
                        {                 
                            Console.WriteLine($"Received message  {result.Message.Value} to {result.TopicPartitionOffset}");
                            KafkaMessage<T> obj = Json.ConvertJsonToObject<KafkaMessage<T>>(result.Message.Value);
                            await saveMessage(result,obj);
                            if (obj != null) {
                                if (obj.IdOffsetResponse == offset)
                                {
                                    response = obj.Data;
                                    break;
                                }
                            }                           
                        }
                    }
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error while consuming message: {e.Error.Reason}");
                }
            }
            consumer.Close();
            return response;
        }

       



        public async Task saveMessage<T>(ConsumeResult<Ignore,string> result,KafkaMessage<T> kf)
        {
            MessageReceivedDTO messag = new MessageReceivedDTO();
            messag.Timestamp = DateTime.Now;
            messag.Message = result.Message.Value;
            messag.Offset = ((int)result.Offset.Value);
            messag.TagMessage = kf.Tag;
            messag.Type = kf.Type;
            if (kf.Type == MessageType.Response.ToString())
                messag.IdOffsetResponse = kf.IdOffsetResponse;
            messag.Topic = topic_response;
            var newItemID = await messRepo.Create(messag);
        }
    }
}
