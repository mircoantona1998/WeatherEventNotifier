from email import utils
import json
from datetime import datetime

from sqlalchemy import DateTime
from DB.Model import Notify
from DB.Repository.NotifyRepo import NotifyRepo
from DB.Repository.ScheduleRepo import ScheduleRepo
from Kafka.KafkaMessage import KafkaMessage
from Utils.EnumMessageType import MessageType
from Utils.Json import Json

class EventHandlers:    
    #NOTIFY
    def handle_tag_GetNotify(offset, data,tag):
        events_Notifys = NotifyRepo.get_all_by_user(data["IdUser"])
        serialized_data = [event_config.as_dict() for event_config in events_Notifys]
        kf = KafkaMessage(offset, MessageType.Response.value, tag, serialized_data)
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data
    
    #CONFIGURATION CHANGE
    def handle_tag_AddConfiguration(offset, data,tag):
        if data["DateTimeActivation"]!=None:
                datetime_obj = datetime.strptime(data["DateTimeActivation"], '%Y-%m-%d %H:%M:%S')
                if  datetime_obj <= datetime.utcnow():
                      new_element_data = {
                        'IdConfiguration': data["Id"],
                        'DateTimeToSchedule': datetime.utcnow(),
                        'ToWork': 1,
                         }
                      ScheduleRepo.add_schedule(new_element_data)
                      kf = KafkaMessage(offset, MessageType.Response.value, tag,  Json.convert_object_to_json(True))
                      json_data = json.dumps(kf.__dict__, indent=2) 
        return json_data
   
    def handle_tag_PatchConfiguration(offset, data,tag):

        #result =FrequencyRepo.get_element(data["IdFrequency"])
        #if result is None:
      #      raise Exception(str("Non esiste la frequenza selezionata"))
       #     return#
       # result =MetricRepo.get_element(data["IdMetric"])
        #if result is None:
         #   raise Exception(str("Non esiste la metrica selezionata"))
          #  return
        new_element_data = {
        'IdConfiguration': data["Id"],
        'ToWork': 1,
            }
        res = ScheduleRepo.patch_schedule(data["Id"],new_element_data)
        kf = KafkaMessage(offset, MessageType.Response.value, tag,  Json.convert_object_to_json(res))
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data
    
    def handle_tag_DeleteConfiguration(offset, data,tag):
        res = ScheduleRepo.delete_schedule(data["IdConfiguration"])
        kf = KafkaMessage(offset, MessageType.Response.value, tag, Json.convert_object_to_json(res))
        json_data = json.dumps(kf.__dict__, indent=2)
        return json_data

    tag_handlers = {
    "GetNotify": handle_tag_GetNotify,
    
    "AddConfiguration": handle_tag_AddConfiguration,
    "PatchConfiguration": handle_tag_PatchConfiguration,
    "DeleteConfiguration": handle_tag_DeleteConfiguration,
    }