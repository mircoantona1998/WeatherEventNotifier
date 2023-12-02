from common.Exceptions import DbConfigurationException
import json
import os.path
import mysql.connector
from mysql.connector.constants import ClientFlag


class BaseCustomizationLoader:

    def __init__(self, CUSTOMIZATION_PATH, PIKA_QUEUE_NAME):
        self.CUSTOMIZATION_PATH = CUSTOMIZATION_PATH
        self.PIKA_QUEUE_NAME = PIKA_QUEUE_NAME

    def load_db(self):
        print(os.path.join(self.CUSTOMIZATION_PATH))
        with open(os.path.join(self.CUSTOMIZATION_PATH, 'config_db.json')) as json_file:
            data = json.load(json_file)
        return '{}://{}:{}@{}:{}/{}'.format(data["dialect"], data["usr"], data["pwd"], data["address"], data["port"],
                                            data["schema"])

    def connect_db(self):
        with open(os.path.join(self.CUSTOMIZATION_PATH, "config_db.json")) as json_file:
            data = json.load(json_file)
            return {'host': data["address"],
                    'user': data["usr"], 
                    'password': data["pwd"],
                    'port':data["port"] , 
                    'client_flags': [mysql.connector.ClientFlag.SSL],
                    'ssl_ca': data["ssl_ca"] }
    def get_loger_init_file_path(self):
        return os.path.join(self.CUSTOMIZATION_PATH, "logging.ini")

    def get_loger_file_path(self):
        with open(os.path.join(self.CUSTOMIZATION_PATH, "config_log.json")) as json_file:
            data = json.load(json_file)
        return data["path"]

    def get_scheduler_config(self):
        with open(os.path.join(self.CUSTOMIZATION_PATH, "config_scheduler.json")) as json_file:
            data = json.load(json_file)
        return data

    def get_pathToSave_config(self):
        with open(os.path.join(self.CUSTOMIZATION_PATH, "config_savefile.json")) as json_file:
            data = json.load(json_file)
        return data

    def get_excluded_log_levels(self):
        with open(os.path.join(self.CUSTOMIZATION_PATH, "config_log.json")) as json_file:
            data = json.load(json_file)
        switcher = {"ERROR": ["WARNING", "INFO", "DEBUG"], "WARNING": ["INFO", "DEBUG"], "INFO": ["DEBUG"], "DEBUG": []}
        return switcher[data["level"]]

    def get_day_config(self):
        with open(os.path.join(self.CUSTOMIZATION_PATH, "config_savefile.json")) as json_file:
            data = json.load(json_file)
        return data

    def get_path_config(self):
        with open(os.path.join(self.CUSTOMIZATION_PATH, "config_path.json")) as json_file:
            data = json.load(json_file)
        return data

    def get_url_config(self):
        with open(os.path.join(self.CUSTOMIZATION_PATH, "config_url.json")) as json_file:
            data = json.load(json_file)
        return data

    def get_db_config(self):
        with open(os.path.join(self.CUSTOMIZATION_PATH, "config_db.json")) as json_file:
            data = json.load(json_file)
        return data