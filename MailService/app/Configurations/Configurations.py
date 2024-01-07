import os
from Utils.Logger import Logger
import inspect
from datetime import datetime
class Configurations:
    _instance = None

    def __new__(cls):
        if cls._instance is None:
            cls._instance = super(Configurations, cls).__new__(cls)
            cls._instance._load_configurations()
        return cls._instance

    def _load_configurations(self):
            Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - load_configurations - {inspect.currentframe().f_globals['__file__']}")
        # Usa le variabili d'ambiente, altrimenti carica default
            self.consumer_bootstrap_servers = os.getenv("CONSUMER_BOOTSTRAP_SERVERS", "127.0.0.1:9092")
            print(f'{str(self.consumer_bootstrap_servers)}')
            self.consumer_group_id = os.getenv("CONSUMER_GROUP_ID", "MailService")
            print(str(self.consumer_group_id))
            self.consumer_auto_offset_reset = os.getenv("CONSUMER_AUTO_OFFSET_RESET", "earliest")
            print(str(self.consumer_auto_offset_reset))
            self.producer_bootstrap_servers = os.getenv("PRODUCER_BOOTSTRAP_SERVERS", "127.0.0.1:9092")
            print(str(self.producer_bootstrap_servers))
            self.producer_client_id = os.getenv("PRODUCER_CLIENT_ID", "MailService")
            print(str(self.producer_client_id))
            self.group_id = os.getenv("GROUP_ID", "MailService")
            print(str(self.group_id))
            self.topic_to_scheduler = os.getenv("TOPIC_TO_SCHEDULER", "topic_to_scheduler")
            print(str(self.topic_to_scheduler))
            self.topic_to_weather = os.getenv("TOPIC_TO_WEATHER", "topic_to_weather")
            print(str(self.topic_to_weather))
            self.topic_to_mail = os.getenv("TOPIC_TO_MAIL", "topic_to_mail")
            print(str(self.topic_to_mail))
            self.topic_to_telegram = os.getenv("TOPIC_TO_TELEGRAM", "topic_to_telegram")
            print(str(self.topic_to_telegram))
            self.topic_to_notifier = os.getenv("TOPIC_TO_NOTIFIER", "topic_to_notifier")
            print(str(self.topic_to_notifier))
            self.topic_to_configuration = os.getenv("TOPIC_TO_CONFIGURATION", "topic_to_configuration")
            print(str(self.topic_to_configuration))
            self.topic_to_userdata = os.getenv("TOPIC_TO_USERDATA", "topic_to_userdata")
            print(str(self.topic_to_userdata))
            self.database_username = os.getenv("DATABASE_USERNAME", "root")
            print(str(self.database_username))
            self.database_password = os.getenv("DATABASE_PASSWORD", "root")
            print(str(self.database_password))
            self.database_porta = os.getenv("DATABASE_PORTA", "3307")
            print(str(self.database_porta))
            self.database_ip = os.getenv("DATABASE_IP", "127.0.0.1")
            print(str(self.database_ip))
            self.database_name = os.getenv("DATABASE_NAME", "Mail")
            print(str(self.consumer_group_id))
       