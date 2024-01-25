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
            Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - load_configurations  - {inspect.currentframe().f_globals['__file__']}")
        # Usa le variabili d'ambiente, altrimenti carica default
            self.prometheus_ip= os.getenv("PROMETHEUS_IP", "localhost")
            print(f'{str(self.prometheus_ip)}')
            self.database_username = os.getenv("DATABASE_USERNAME", "sa")
            print(str(self.database_username))
            self.database_password = os.getenv("DATABASE_PASSWORD", "RootRoot.1")
            print(str(self.database_password))
            self.database_porta = os.getenv("DATABASE_PORTA", "1434")
            print(str(self.database_porta))
            self.database_ip = os.getenv("DATABASE_IP", "127.0.0.1")
            print(str(self.database_ip))
            self.database_name = os.getenv("DATABASE_NAME", "SLAManager")
            print(str(self.database_name))
       