from Configurations.Configurations import Configurations
from Kafka.Consumer import ConsumerClass
import time
from Utils.Logger import Logger
import inspect
from datetime import datetime
if __name__ == "__main__":
    Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - avvio0 Notifier service - {inspect.currentframe().f_globals['__file__']}")
    Configurations()
    time.sleep(60)
    ConsumerClass().run_request()