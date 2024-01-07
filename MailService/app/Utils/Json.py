import json
from Utils.Logger import Logger
import inspect
from datetime import datetime
class Json:
    @staticmethod
    def custom_json_serializer(obj):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - custom_json_serializer - {inspect.currentframe().f_globals['__file__']}")
        if isinstance(obj, datetime):
            return obj.strftime('%Y-%m-%d %H:%M:%S')
        raise TypeError("Type not serializable")
    
    @staticmethod
    def convert_object_to_json(obj):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - convert_object_to_json - {inspect.currentframe().f_globals['__file__']}")
        json_options = {
            "indent": 2,
            "default": Json.custom_json_serializer
        }
        json_string = json.dumps(obj, **json_options)
        return json_string

    @staticmethod
    def convert_json_to_object(json_string, object_type):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - convert_json_to_object - {inspect.currentframe().f_globals['__file__']}")
        def from_camel_case(name):
            return ''.join([x.capitalize() if i > 0 else x for i, x in enumerate(name.split('_'))])

        def convert_data(data):
            return json.loads(data)

        return json.loads(json_string, object_hook=lambda d: object_type(
            IdOffsetResponse=d.get(from_camel_case('IdOffsetResponse')),
            Type=d.get(from_camel_case('Type')),
            Tag=d.get(from_camel_case('Tag')),
            Data=convert_data(d.get(from_camel_case('Data')))
        ))

    
    # @staticmethod
    # def leggi_configurazioni( nome_configurazione):
    #     with open("config.json", 'r') as file:
    #         configurazioni = json.load(file)
    #     configurazione_specificata = configurazioni.get(nome_configurazione, None)
    #     if configurazione_specificata is None:
    #         raise ValueError(f"Configurazione '{nome_configurazione}' non trovata nel file.")
    #     return configurazione_specificata