using Confluent.Kafka;
using ExposeAPI.DB;
using ExposeAPI.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Dynamic;
using System.Reflection.PortableExecutable;
using System.Text;
using Userdata.Models;
using Userdata.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExposeAPI.Kafka
{
    public class KafkaConsumer
    {
        private readonly string topic;
        private readonly MessageReceivedRepository messRepo;

        public KafkaConsumer(string topic)
        {
            messRepo = new MessageReceivedRepository(config.confdb);
            this.topic = topic;
        }

        public async Task<T> ConsumeResponse<T>(int offset)
        {
            T response = default(T);
            var config = new ConsumerConfig
            {
                GroupId = Kafka.consumerConfig.GroupId,
                BootstrapServers = Kafka.consumerConfig.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(topic);
            while (true)
            {
                try
                {
                    var result = consumer.Consume(TimeSpan.FromMilliseconds(100));
                    if (result != null)
                    {
                        Console.WriteLine($"Received message  {result.Message.Value} on {result.TopicPartitionOffset}");
                        var headers = result.Message.Headers;
                        var messLast = await messRepo.GetLast();
                        if (messLast == null || ((int)result.Offset.Value) > messLast.Offset)
                        {
                            if (headers != null && headers.Count > 0)
                            {
                                try
                                {
                                    var headerKafka = new KafkaHeader(headers, result.TopicPartition.Partition);
                                    if (headerKafka.IdOffsetResponse == offset)
                                    {
                                        Dictionary<string, T> dictionary = JsonConvert.DeserializeObject<Dictionary<string, T>>(result.Message.Value);
                                        var obj = dictionary.Values.First();
                                        await saveMessage(result, headerKafka);
                                        if (obj != null)
                                        {
                                            response = obj;
                                            break;
                                        }
                                    }
                                }
                                catch
                                {
                                    await saveMessageWithError(result);
                                }             
                            }
                        }
                        else
                        {
                            await saveMessageWithError(result);
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


        public async Task saveMessage(ConsumeResult<Ignore,string> result,KafkaHeader headers)
        {
            MessageReceivedDTO messag = new MessageReceivedDTO();
            messag.Timestamp = DateTime.UtcNow;
            messag.Message = result.Message.Value;
            messag.Offset = ((int)result.Offset.Value);
            messag.Creator = headers.Creator;
            messag.IdOffsetResponse = headers.IdOffsetResponse;
            messag.Type = headers.Type;
            messag.TagMessage = headers.Tag;
            messag.Code = headers.Code;
            messag.Topic = result.Topic;
            messag.Partition = result.Partition;
            var newItemID = await messRepo.Create(messag);
        }
        public async Task saveMessageWithError(ConsumeResult<Ignore, string> result)
        {
            MessageReceivedDTO messag = new MessageReceivedDTO();
            messag.Timestamp = DateTime.UtcNow;
            messag.Message = result.Message.Value;
            messag.Offset = ((int)result.Offset.Value);
            messag.Topic = result.Topic;
            messag.Code =MessageCode.Error.ToString();
            messag.Partition = result.Partition;
            var newItemID = await messRepo.Create(messag);
        }
    }
}
