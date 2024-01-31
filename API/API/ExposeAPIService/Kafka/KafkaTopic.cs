using Confluent.Kafka.Admin;
using Confluent.Kafka;
using ExposeAPI.Utils;

namespace ExposeAPI.Kafka
{
    public static class  KafkaTopic
    {
        public async static  Task ConfigurationTopicPertitions(string topic_kafka,int numPartitions,AdminClientConfig adminClientConfig)
        {
            Logger log = new();
            using (var adminClient = new AdminClientBuilder(adminClientConfig).Build())
            {
                bool topicCreated = false;

                while (!topicCreated)
                {
                    try
                    {
                        var topicSpecification = new TopicSpecification
                        {
                            Name = topic_kafka,
                            NumPartitions = numPartitions,
                            ReplicationFactor = 1
                        };

                        adminClient.CreateTopicsAsync(new List<TopicSpecification> { topicSpecification }).Wait();
                        Console.WriteLine($"Topic '{topic_kafka}' created successfully.");
                        log.LogAction($"Topic '{topic_kafka}' created successfully.");
                        topicCreated = true;
                    }
                    catch (AggregateException ae)
                    {
                        foreach (var innerException in ae.InnerExceptions)
                        {
                            if (innerException is CreateTopicsException createTopicsException)
                            {
                                foreach (var topicError in createTopicsException.Results)
                                {
                                    if (topicError.Error.Code == ErrorCode.TopicAlreadyExists)
                                    {
                                        try
                                        {
                                            var topicMetadata = adminClient.GetMetadata(topic_kafka, TimeSpan.FromSeconds(5));
                                            var currentPartitionCount = topicMetadata.Topics[0].Partitions.Count;
                                            if (numPartitions > currentPartitionCount)
                                            {
                                                var partitionsToAdd = numPartitions - currentPartitionCount;
                                                var partitionsSpecification = new List<PartitionsSpecification>
                                                {
                                                    new PartitionsSpecification
                                                    {
                                                        Topic = topic_kafka,
                                                        IncreaseTo = numPartitions
                                                    }
                                                };
                                                adminClient.CreatePartitionsAsync(partitionsSpecification);
                                                Console.WriteLine($"Partitions of topic '{topic_kafka}' increased to {numPartitions}.");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"The current partition count ({currentPartitionCount}) is already equal or greater than the desired partition count ({numPartitions}).");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine($"Error: {ex.Message}");
                                        }
                                        Console.WriteLine($"The topic '{topic_kafka}' already exists.");
                                        log.LogAction($"The topic '{topic_kafka}' already exists.");
                                        topicCreated = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Error creating topic: {topicError.Error.Reason}");
                                        log.LogAction($"Error creating topic: {topicError.Error.Reason}");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Unexpected exception: {innerException.Message}");
                                log.LogAction($"Unexpected exception: {innerException.Message}");
                            }
                        }
                    }
                }
            }
        }
    }
}
