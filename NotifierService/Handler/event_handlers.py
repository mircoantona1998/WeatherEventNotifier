from email import utils
import json
from datetime import datetime
from Configurations.Configurations import Configurations
from DB.Repository.NotifyRepo import NotifyRepo
from Handler.event_destinators import GestoreDestinatari
from Kafka.KafkaHeader import KafkaHeader
from Kafka.Producer import ProducerClass
from Utils.EnumMessageCode import MessageCode
from Utils.EnumMessageType import MessageType

class EventHandlers:    
    #NOTIFY
    def handle_tag_GetNotify(data):
           return NotifyRepo.get_all_by_user(data["IdUser"])
    
    #SCHEDULER
    def handle_tag_SchedulationCurrentHour(dat):
        #passare richiesta al weather service per far valutare
        data = dat["Data"]
        new_element_data = {
        'IdSchedule': data["Id"],
        'IdConfiguration': data["IdConfiguration"],
        'DateTimeToSchedule': data["DateTimeToSchedule"],
        'FieldMetric':data["FieldMetric"],
        'Symbol':data["Symbol"],
        'Value':data["Value"],
        'IdUser':data["IdUser"],
        'Latitude':data["Latitude"],
        'Longitude':data["Longitude"],
        'ParentMetric':data["ParentMetric"],
            }
        headersRequest= KafkaHeader(IdOffsetResponse=-1,Type=MessageType.Request.value ,Tag="AnalyzeConfiguration", Creator=Configurations().group_id, Code = MessageCode.Ok.value)
        ProducerClass.send_message(headersRequest.headers_list,json.dumps({'Data': new_element_data}, indent=2),GestoreDestinatari().determina_destinatario("WeatherService"))               
        return 
    
    #GENERA NOTIFICA    
    def handle_tag_GenerateNotification(dat):     
        data = dat["Data"]
        if data["Notify"]==True:
            new_element_data = {
                'IdUser': data["IdUser"],
                'IdSchedule': data["IdSchedule"], 
                'Message': "Attenzione per la configurazione. Valore misurato: " +str(data["ValueWeather"]),
                'DateTimeCreate': datetime.utcnow(),
                'IdConfiguration': data["IdConfiguration"], 
                'ValueWeather': data["ValueWeather"]
            }
            NotifyRepo.add_element(new_element_data)
            headersRequest= KafkaHeader(IdOffsetResponse=-1,Type=MessageType.Request.value ,Tag="NewTip", Creator=Configurations().group_id, Code = MessageCode.Ok.value)
            ProducerClass.send_message(headersRequest.headers_list,{'Data': new_element_data},GestoreDestinatari().determina_destinatario("TipService")) 
            ProducerClass.send_message(headersRequest.headers_list,{'Data': new_element_data},GestoreDestinatari().determina_destinatario("MailService")) 
            ProducerClass.send_message(headersRequest.headers_list,{'Data': new_element_data},GestoreDestinatari().determina_destinatario("TelegramService")) 
        return 
    
    tag_handlers = {
    "GetNotify": handle_tag_GetNotify,
    "SchedulationCurrentHour": handle_tag_SchedulationCurrentHour,
    "GenerateNotification": handle_tag_GenerateNotification,
    }