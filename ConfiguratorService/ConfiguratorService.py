from Configurations.Configurations import Configurations
from Kafka.Consumer import ConsumerClass
from Kafka.Producer import ProducerClass

if __name__ == "__main__":
    Configurations()
    ConsumerClass().run_request()