from Configurations.Configurations import Configurations
from Kafka.Consumer import ConsumerClass
from Utils.Logger import Logger
import inspect
from datetime import datetime
from DB.Session import Session
if __name__ == "__main__":
    Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - avvio Forecast service  - {inspect.currentframe().f_globals['__file__']}")
    Configurations()
    Session.wait_for_sql_server()
    ConsumerClass().run_request()
    

