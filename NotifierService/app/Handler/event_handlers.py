from email import utils
import json
import pytz
from Utils.Logger import Logger
import inspect
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
           Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_GetNotify - {inspect.currentframe().f_globals['__file__']}")
           return NotifyRepo.get_all_by_user(data["IdUser"])
    
    #SCHEDULER
    # def handle_tag_SchedulationCurrentHour(dat):
    #     Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_SchedulationCurrentHour - {inspect.currentframe().f_globals['__file__']}")
    #     #passare richiesta al weather service per far valutare
    #     data = dat["Data"]
    #     new_element_data = {
    #     'IdSchedule': data["Id"],
    #     'NameConfiguration': data["NameConfiguration"],
    #     'IdConfiguration': data["IdConfiguration"],
    #     'DateTimeToSchedule': data["DateTimeToSchedule"],
    #     'FieldMetric':data["FieldMetric"],
    #     'Symbol':data["Symbol"],
    #     'Value':data["Value"],
    #     'IdUser':data["IdUser"],
    #     'Latitude':data["Latitude"],
    #     'Longitude':data["Longitude"],
    #     'ParentMetric':data["ParentMetric"],
    #     'ValueUnit':data["ValueUnit"],
    #     'Description':data["Description"],
    #         }
    #     headersRequest= KafkaHeader(IdOffsetResponse=-1,Type=MessageType.Request.value ,Tag="AnalyzeConfiguration", Creator=Configurations().group_id, Code = MessageCode.Ok.value)
    #     ProducerClass.send_message(headersRequest.headers_list,json.dumps({'Data': new_element_data}, indent=2),GestoreDestinatari().determina_destinatario("WeatherService"))
    #     print(f'{str(headersRequest.to_string())}')
    #     Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - {str(headersRequest.to_string())} - {inspect.currentframe().f_globals['__file__']}")
    #     return 
    
    #GENERA NOTIFICA    
    def handle_tag_GenerateNotification(dat):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_GenerateNotification - {inspect.currentframe().f_globals['__file__']}")
        data = dat["Data"]
        fuso_orario_locale = pytz.timezone('Europe/Rome')  
        data_datetime_locale = datetime.strptime(data["DateTimeToSchedule"], '%Y-%m-%d %H:%M:%S').replace(tzinfo=pytz.UTC)
        data_datetime_locale = data_datetime_locale.astimezone(fuso_orario_locale)
        if data["Notify"]==True:
            new_element_data = {
                'IdUser': data["IdUser"],
                'IdSchedule': data["Id"], 
                'Message': "Attenzione per la configurazione: '"+str(data["NameConfiguration"])+"'. Valore misurato : " +str(data["ValueWeather"])+" "+str(data["ValueUnit"])+" ("+str(data["Description"])+" "+str( data_datetime_locale.strftime("%d-%m-%Y %H:%M"))+")",
                'DateTimeCreate': datetime.utcnow().strftime('%Y-%m-%d %H:%M:%S'),
                'IdConfiguration': data["IdConfiguration"], 
                'ValueWeather': data["ValueWeather"]
            }
            NotifyRepo.add_element(new_element_data)
            headersRequest= KafkaHeader(IdOffsetResponse=-1,Type=MessageType.Request.value ,Tag="NewTip", Creator=Configurations().group_id, Code = MessageCode.Ok.value)
            ProducerClass.send_message(headersRequest.headers_list,json.dumps({'Data': new_element_data}, indent=2),GestoreDestinatari().determina_destinatario("MailService")) 
            print(f'{str(headersRequest.to_string())}')
            Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - {str(headersRequest.to_string())} - {inspect.currentframe().f_globals['__file__']}")
            ProducerClass.send_message(headersRequest.headers_list,json.dumps({'Data': new_element_data}, indent=2),GestoreDestinatari().determina_destinatario("TelegramService")) 
            print(f'{str(headersRequest.to_string())}')
            Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - {str(headersRequest.to_string())} - {inspect.currentframe().f_globals['__file__']}")
        return 
    
    tag_handlers = {
    "GetNotify": handle_tag_GetNotify,
   # "SchedulationCurrentHour": handle_tag_SchedulationCurrentHour,
    "GenerateNotification": handle_tag_GenerateNotification,
    }