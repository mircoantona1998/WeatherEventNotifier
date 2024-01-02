from Kafka.Consumer import ConsumerClass
from Kafka.Producer import ProducerClass
from Configurations.Configurations import Configurations

if __name__ == "__main__":
    Configurations()
    ConsumerClass().run_request()
    