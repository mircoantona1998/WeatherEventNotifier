from Configurations.Configurations import Configurations
from Kafka.Consumer import ConsumerClass
import time
if __name__ == "__main__":
    Configurations()
    time.sleep(60)
    ConsumerClass().run_request()