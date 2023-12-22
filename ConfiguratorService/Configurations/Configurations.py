import json

class Configurations:
    _instance = None
    def __new__(cls, file_path="config.json"):
        if cls._instance is None:
            cls._instance = super(Configurations, cls).__new__(cls)
            cls._instance._load_configurations(file_path)
        return cls._instance
    def  _load_configurations(self, file_path="config.json"):
        with open(file_path, "r") as file:
            config_data = json.load(file)
        self.consumer=config_data["configuration_consumer"]
        self.consumer_bootstrap_servers = config_data["configuration_consumer"]["bootstrap.servers"]
        self.consumer_group_id = config_data["configuration_consumer"]["group.id"]
        self.consumer_auto_offset_reset = config_data["configuration_consumer"]["auto.offset.reset"]
        self.producer=config_data["configuration_producer"]
        self.producer_bootstrap_servers = config_data["configuration_producer"]["bootstrap.servers"]
        self.producer_client_id = config_data["configuration_producer"]["client.id"]
        self.group_id = config_data["group.id"]
        self.topic_to_notifier = config_data["topic_to_notifier"]
        self.topic_to_configuration = config_data["topic_to_configuration"]
        self.topic_to_userdata = config_data["topic_to_userdata"]
        self.database=config_data["database"]
        self.database_username = config_data["database"]["username"]
        self.database_password = config_data["database"]["password"]
        self.database_porta = config_data["database"]["porta"]
        self.database_ip = config_data["database"]["ip"]
        self.database_name = config_data["database"]["database"]



