using Confluent.Kafka;
using ExposeAPI.Configurations;
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
        Logger log = new();

        public KafkaConsumer(string topic)
        {
            log.LogAction("Kafka consumer");
            messRepo = new MessageReceivedRepository(config.confdb);
            this.topic = topic;
        }

        public async Task<T> ConsumeResponse<T>(int offset,string cluster)
        {
            T response = default(T);
            Kafka.consumerConfig = new ConsumerConfig
            {
                BootstrapServers = Environment.GetEnvironmentVariable(cluster) ?? ExposeAPI.Configurations.config.configuration["bootstrapServers"],
                GroupId = Environment.GetEnvironmentVariable("groupID") ?? ExposeAPI.Configurations.config.configuration["groupID"],
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true
            };

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
                        log.LogAction($"Received message  {result.Message.Value} on {result.TopicPartitionOffset}");
                        var headers = result.Message.Headers;
                        //var messLast = await messRepo.GetLast();
                       // if (messLast == null || ((int)result.Offset.Value) > messLast.Offset)
                       // {
                            if (headers != null && headers.Count > 0)
                            {
                                try
                                {
                                    var headerKafka = new KafkaHeader(headers, result.TopicPartition.Partition);
                                    log.LogAction(headerKafka.ToString());
                                    if (headerKafka.IdOffsetResponse == offset)
                                    {
                                        bool save = false;
                                        try
                                        {
                                            Dictionary<string, T> dictionary = JsonConvert.DeserializeObject<Dictionary<string, T>>(result.Message.Value);
                                            var obj = dictionary.Values.First();
                                            await saveMessage(result, headerKafka);
                                            consumer.Commit(result);
                                            save = true;
                                            if (obj != null)
                                            {
                                                response = obj;
                                                break;
                                            }
                                        }
                                        catch
                                        {
                                            if (save == false)
                                            {
                                                await saveMessageWithError(result);
                                                consumer.Commit(result);
                                            }
                                            break;
                                        }
                                    }
                                }
                                catch
                                {
                                    await saveMessageWithError(result);
                                    consumer.Commit(result);
                                    break;
                                }             
                            }
                       // }
                       // else
                       // {
                       //     await saveMessageWithError(result);
                       // }
                    }
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error while consuming message: {e.Error.Reason}");
                    log.LogAction($"Error while consuming message: {e.Error.Reason}");
                }
            }
            consumer.Close();
            return response;
        }


        public async Task saveMessage(ConsumeResult<Ignore,string> result,KafkaHeader headers)
        {
            log.LogAction("Save message");
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
            log.LogAction("Save MessageWithError");
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
