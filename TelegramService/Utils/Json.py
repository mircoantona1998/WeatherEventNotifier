import json

class Json:
    @staticmethod
    def convert_object_to_json(obj):
        json_options = {
            "indent": 2
        }
        json_string = json.dumps(obj, **json_options)
        return json_string

    @staticmethod
    def convert_json_to_object(json_string, object_type):
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

    @staticmethod
    def packaging_message(element, message_type, message_tag, id_message):
        data = Json.convert_object_to_json(element)
        json_data = {
            "IdOffsetResponse": id_message,
            "Type": message_type,
            "Tag": message_tag,
            "Data": data
        }
        json_string = json.dumps(json_data, indent=2)
        return json_string
    
    @staticmethod
    def leggi_configurazioni( nome_configurazione):
        with open("config.json", 'r') as file:
            configurazioni = json.load(file)
        configurazione_specificata = configurazioni.get(nome_configurazione, None)
        if configurazione_specificata is None:
            raise ValueError(f"Configurazione '{nome_configurazione}' non trovata nel file.")
        return configurazione_specificata