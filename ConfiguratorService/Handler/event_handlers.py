import json
from datetime import datetime
from DB.Model import Frequency
from DB.Repository.EventsConfigurationRepo import EventsConfigurationRepo
from DB.Repository.MetricRepo import MetricRepo
from DB.Repository.FrequencyRepo import FrequencyRepo
from Kafka.KafkaMessage import KafkaMessage
from Utils.EnumMessageType import MessageType
class EventHandlers:
    #CONFIGURATION
    def handle_tag_AddConfiguration(offset, data,tag):
        dtActivation=data["DateTimeActivation"]
        if dtActivation!=None:
            datetime_obj = datetime.strptime(dtActivation, '%Y-%m-%dT%H:%M:%S.%fZ')
            formatted_datetime = datetime_obj.strftime('%Y-%m-%d %H:%M:%S')
        else: 
            formatted_datetime= datetime.utcnow()
        if data["IdUser"]==None or data["IdUser"]==0:
            raise Exception(str("Il campo IdUser non puo essere vuoto"))
            return
        if data["IdFrequency"]==None or data["IdFrequency"]==0:
            raise Exception(str("Il campo IdFrequency non puo essere vuoto"))
            return
        if data["Longitude"]==None or data["Longitude"]==0:
            raise Exception(str("Il campo Longitude non puo essere vuoto"))
            return
        if data["Latitude"]==None or data["Latitude"]==0:
            raise Exception(str("Il campo Latitude non puo essere vuoto"))
            return
        if data["IdMetric"]==None or data["IdMetric"]==0:
            raise Exception(str("Il campo IdMetric non puo essere vuoto"))
            return
        result =FrequencyRepo.get_element(data["IdFrequency"])
        if result is None:
            raise Exception(str("Non esiste la frequenza selezionata"))
            return
        result =MetricRepo.get_element(data["IdMetric"])
        if result is None:
            raise Exception(str("Non esiste la metrica selezionata"))
            return
        new_element_data = {
            'IdUser': data["IdUser"],
            'IdFrequency': data["IdFrequency"], 
            'Longitude': data["Longitude"],
            'Latitude': data["Latitude"],
            'DateTimeCreate': datetime.utcnow(),
            'DateTimeUpdate': datetime.utcnow(),
            'DateTimeActivation': formatted_datetime, 
            'IsActive': True,
            'IdMetric': data["IdMetric"], 
        }
        EventsConfigurationRepo.add_element(new_element_data)
        kf = KafkaMessage(offset, MessageType.Response.value, tag, True)
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data

    def handle_tag_PatchConfiguration(offset, data,tag):
        dtActivation=data["DateTimeActivation"]
        if dtActivation!=None:
            datetime_obj = datetime.strptime(dtActivation, '%Y-%m-%dT%H:%M:%S.%fZ')
            formatted_datetime = datetime_obj.strftime('%Y-%m-%d %H:%M:%S')
        else: 
            formatted_datetime= datetime.utcnow()
        result =FrequencyRepo.get_element(data["IdFrequency"])
        if result is None:
            raise Exception(str("Non esiste la frequenza selezionata"))
            return
        result =MetricRepo.get_element(data["IdMetric"])
        if result is None:
            raise Exception(str("Non esiste la metrica selezionata"))
            return
        new_element_data = {
            'IdUser': data["IdUser"],
            'IdFrequency': data["IdFrequency"], 
            'Longitude': data["Longitude"],
            'Latitude': data["Latitude"],
            'DateTimeUpdate': datetime.utcnow(),
            'DateTimeActivation': formatted_datetime,  
            'IsActive': True,
            'IdMetric': data["IdMetric"] 
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
        if data["IdUser"]==None or data["IdUser"]==0:
            raise Exception(str("Il campo IdUser non puo essere vuoto"))
            return
        if data["IdConfiguration"]==None or data["IdConfiguration"]==0:
            raise Exception(str("Il campo IdConfiguration non puo essere vuoto"))
            return
        EventsConfigurationRepo.delete_element(data["IdUser"], data["IdConfiguration"])
        kf = KafkaMessage(offset, MessageType.Response.value, tag, True)
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data

    #METRIC
    def handle_tag_AddMetric(offset, data,tag):
        result =MetricRepo.get_element_by_field(data["Field"])
        if result is not None:
            raise Exception(str("La metrica esiste nel database"))
            return
        if data["Field"]==None or data["Field"]==0:
            raise Exception(str("Il campo Field non puo essere vuoto"))
            return
        if data["Type"]==None or data["Type"]==0:
            raise Exception(str("Il campo Type non puo essere vuoto"))
            return
        if data["ValueUnit"]==None or data["ValueUnit"]==0:
            raise Exception(str("Il campo ValueUnit non puo essere vuoto"))
            return
        new_element_data = {
            'Field': data["Field"], 
            'Type': data["Type"],
            'ValueUnit': data["ValueUnit"],
            'IsActive': data["IsActive"]
        }
        MetricRepo.add_element(new_element_data)
        kf = KafkaMessage(offset, MessageType.Response.value, tag, True)
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data

    def handle_tag_PatchMetric(offset, data,tag):
        result =MetricRepo.get_element_by_field(data["Field"])
        if result is not None:
            raise Exception(str("La metrica esiste nel database"))
            return
        new_element_data = { 
            'Field': data["Field"],
            'Type': data["Type"],
            'ValueUnit': data["ValueUnit"],
            'IsActive': data["IsActive"]
        }
        MetricRepo.patch_element(data["IdMetric"], new_element_data)
        kf = KafkaMessage(offset,MessageType.Response.value, tag, True)
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data

    def handle_tag_GetMetric(offset, data,tag):
        events_Metrics = MetricRepo.get_all()
        serialized_data = [event_config.as_dict() for event_config in events_Metrics]
        kf = KafkaMessage(offset, MessageType.Response.value, tag, serialized_data)
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data

    def handle_tag_DeleteMetric(offset, data,tag):
        if data["IdMetric"]==None or data["IdMetric"]==0:
            raise Exception(str("Il campo IdMetric non puo essere vuoto"))
            return
        MetricRepo.delete_element( data["IdMetric"])
        kf = KafkaMessage(offset, MessageType.Response.value, tag, True)
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data
    
    #FREQUENCY
    def handle_tag_AddFrequency(offset, data,tag):
        result =FrequencyRepo.get_element_by_frequency_name(data["FrequencyName"])
        if result is not None:
            raise Exception(str("La frequenza esiste nel database"))
            return
        if data["FrequencyName"]==None or data["FrequencyName"]==0:
            raise Exception(str("Il campo FrequencyName non puo essere vuoto"))
            return
        if data["Minutes"]==None or data["Minutes"]==0:
            raise Exception(str("Il campo Minutes non puo essere vuoto"))
            return
        new_element_data = {
            'FrequencyName': data["FrequencyName"], 
            'Minutes': data["Minutes"],
            'IsActive': data["IsActive"]
        }
        FrequencyRepo.add_element(new_element_data)
        kf = KafkaMessage(offset, MessageType.Response.value, tag, True)
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data

    def handle_tag_PatchFrequency(offset, data,tag):
        result =FrequencyRepo.get_element_by_frequency_name(data["FrequencyName"])
        if result is not None:
            raise Exception(str("La frequenza esiste nel database"))
            return
        new_element_data = {
            'FrequencyName': data["FrequencyName"], 
            'Minutes': data["Minutes"],
            'IsActive': data["IsActive"]
        }
        FrequencyRepo.patch_element(data["IdFrequency"], new_element_data)
        kf = KafkaMessage(offset,MessageType.Response.value, tag, True)
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data

    def handle_tag_GetFrequency(offset, data,tag):
        events_Frequencys = FrequencyRepo.get_all()
        serialized_data = [event_config.as_dict() for event_config in events_Frequencys]
        kf = KafkaMessage(offset, MessageType.Response.value, tag, serialized_data)
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data

    def handle_tag_DeleteFrequency(offset, data,tag):
        if data["IdFrequency"]==None or data["IdFrequency"]==0:
            raise Exception(str("Il campo IdFrequency non puo essere vuoto"))
            return
        FrequencyRepo.delete_element( data["IdFrequency"])
        kf = KafkaMessage(offset, MessageType.Response.value, tag, True)
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data  

    tag_handlers = {
    "AddConfiguration": handle_tag_AddConfiguration,
    "PatchConfiguration": handle_tag_PatchConfiguration,
    "GetConfiguration": handle_tag_GetConfiguration,
    "DeleteConfiguration": handle_tag_DeleteConfiguration,
    
    "AddMetric": handle_tag_AddMetric,
    "PatchMetric": handle_tag_PatchMetric,
    "GetMetric": handle_tag_GetMetric,
    "DeleteMetric": handle_tag_DeleteMetric,
    
    "AddFrequency": handle_tag_AddFrequency,
    "PatchFrequency": handle_tag_PatchFrequency,
    "GetFrequency": handle_tag_GetFrequency,
    "DeleteFrequency": handle_tag_DeleteFrequency,
    }