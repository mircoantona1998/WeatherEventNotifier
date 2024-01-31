from Configurations.Configurations import Configurations
from Kafka.Consumer import ConsumerClass
from Utils.Logger import Logger
import inspect
from datetime import datetime
from DB.Session import Session as DBSession
from DBUsers.Session import Session as DBusers
if __name__ == "__main__":
    Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - avvio Scheduler service  - {inspect.currentframe().f_globals['__file__']}")
    Configurations()
    DBusers.wait_for_sql_server()
    DBSession.wait_for_mysql()
    ConsumerClass().run_request()
    

