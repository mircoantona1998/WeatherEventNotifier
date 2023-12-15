using Confluent.Kafka;

namespace ExposeAPI.Kafka
{
    public static class Kafka
    {
        public static KafkaConsumer consumer;
        public static KafkaProducer producer;
        public static ConsumerConfig consumerConfig;
        public static ProducerConfig producerConfig;
        public static string topic_to_configuration;
        public static string topic_to_userdata;
    }
}
