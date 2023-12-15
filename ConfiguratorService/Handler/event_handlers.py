import json
from datetime import datetime
from DB.Repository.EventsConfigurationRepo import EventsConfigurationRepo
from Kafka.KafkaMessage import KafkaMessage
from Utils.EnumMessageType import MessageType
class EventHandlers:
        
    def handle_tag_AddConfiguration(offset, data,tag):
        new_element_data = {
            'IdUser': data["IdUser"],
            'IdFrequencyType': 2,  # calcolare
            'Longitude': data["Longitude"],
            'Latitude': data["Latitude"],
            'DateTimeCreate': datetime.now(),
            'DateTimeUpdate': datetime.now(),
            'DateActivation': datetime.now(),
            'IsActive': True,
            'IdMetric': 3  # calcolare
        }
        EventsConfigurationRepo.add_element(new_element_data)
        kf = KafkaMessage(offset, MessageType.Response.value, tag, True)
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data

    def handle_tag_PatchConfiguration(offset, data,tag):
        new_element_data = {
            'IdUser': data["IdUser"],
            'IdFrequencyType': 2,
            'Longitude': data["Longitude"],
            'Latitude': data["Latitude"],
            'DateTimeUpdate': datetime.now(),
            'IsActive': True,
            'IdMetric': 3
        }
        EventsConfigurationRepo.patch_element(data["IdConfiguration"], new_element_data)
        kf = KafkaMessage(offset,MessageType.Response.value, tag, True)
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data

    def handle_tag_GetConfiguration(offset, data,tag):
        events_configurations = EventsConfigurationRepo.get_all_by_user(data["IdUser"])
        serialized_data = [event_config.as_dict() for event_config in events_configurations]
        kf = KafkaMessage(offset, MessageType.Response.value, tag, serialized_data)
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data

    def handle_tag_DeleteConfiguration(offset, data,tag):
        EventsConfigurationRepo.delete_element(data["IdUser"], data["IdConfiguration"])
        kf = KafkaMessage(offset, MessageType.Response.value, tag, True)
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data

    tag_handlers = {
    "AddConfiguration": handle_tag_AddConfiguration,
    "PatchConfiguration": handle_tag_PatchConfiguration,
    "GetConfiguration": handle_tag_GetConfiguration,
    "DeleteConfiguration": handle_tag_DeleteConfiguration,
    }