import json
from datetime import datetime
from Configurations.Configurations import Configurations
from DB.Repository.ConfigurationUserRepo import ConfigurationUserRepo
from DB.Repository.MetricRepo import MetricRepo
from DB.Repository.FrequencyRepo import FrequencyRepo
from Handler.event_destinators import GestoreDestinatari
from Kafka.KafkaHeader import KafkaHeader
from Kafka.Producer import ProducerClass
from Utils.EnumMessageCode import MessageCode
from Utils.EnumMessageType import MessageType
from Utils.Json import Json
from Utils.Logger import Logger
import inspect

class EventHandlers:
    
    #CONFIGURATION
    def handle_tag_AddConfiguration( data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_AddConfiguration  - {inspect.currentframe().f_globals['__file__']}")
        dtActivation=data["DateTimeActivation"]
        if dtActivation!=None:
            try:
                datetime_obj = datetime.strptime(dtActivation, '%Y-%m-%dT%H:%M:%S.%fZ')
            except ValueError:
                datetime_obj = datetime.strptime(dtActivation, '%Y-%m-%dT%H:%M:%SZ')
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
        if data["Symbol"]==None or data["Symbol"]==0:
            raise Exception(str("Il campo Symbol non puo essere vuoto"))
            return       
        if data["NameConfiguration"]==None or data["NameConfiguration"]==0:
            raise Exception(str("Il campo NameConfiguration non puo essere vuoto"))
            return
        else:
            result =ConfigurationUserRepo.get_element_by_NameConfiguration(data["NameConfiguration"],data["IdUser"])
            if result is not None:
                raise Exception(str("La NameConfiguration esiste per utenza"))
                return
        if data["Value"]==None:
            data["Value"]==0
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
            'NameConfiguration': data["NameConfiguration"], 
            'IdFrequency': data["IdFrequency"], 
            'Longitude': data["Longitude"],
            'Latitude': data["Latitude"],
            'DateTimeCreate': datetime.utcnow(),
            'DateTimeUpdate': datetime.utcnow(),
            'DateTimeActivation': formatted_datetime, 
            'IsActive': True,
            'IdMetric': data["IdMetric"], 
            'Symbol': data["Symbol"], 
            'Value': data["Value"]
        }
        new_element_data=ConfigurationUserRepo.add_element(new_element_data)
        kf = True
        json_data = json.dumps(kf)
        
        #avviso notifier service che e´ stata aggiunta una configurazione
        new_element_data=ConfigurationUserRepo.get_new_configuration_for_today(new_element_data["Id"])
        headersRequest = KafkaHeader(IdOffsetResponse=-1,Type=MessageType.Request.value ,Tag="AddConfiguration", Creator=Configurations().group_id, Code = MessageCode.Ok.value)
        forNotifier=FrequencyRepo.get_element(data["IdFrequency"])
        new_element_data['Minutes']=forNotifier.Minutes
        ProducerClass.send_message(headersRequest.headers_list,Json.convert_object_to_json(new_element_data),GestoreDestinatari().determina_destinatario("SchedulerService"))        
        print(f'{str(headersRequest.to_string())}')
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - {str(headersRequest.to_string())} - {inspect.currentframe().f_globals['__file__']}")
        return json_data

    def handle_tag_PatchConfiguration( data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_PatchConfiguration  - {inspect.currentframe().f_globals['__file__']}")
        dtActivation=data["DateTimeActivation"]
        if dtActivation!=None:
            try:
                datetime_obj = datetime.strptime(dtActivation, '%Y-%m-%dT%H:%M:%S.%fZ')
            except ValueError:
                datetime_obj = datetime.strptime(dtActivation, '%Y-%m-%dT%H:%M:%SZ')
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
        if data["NameConfiguration"]==None or data["NameConfiguration"]==0:
            raise Exception(str("Il campo NameConfiguration non puo essere vuoto"))
            return
        else:
            result =ConfigurationUserRepo.get_element_by_NameConfiguration(data["NameConfiguration"],data["IdUser"])
            if result is not None:
                if result.Id !=data["IdConfiguration"]:
                    raise Exception(str("Il nome della configurazione gia presente per utenza"))
                    return
        new_element_data = {
            'IdUser': data["IdUser"],
            'NameConfiguration': data["NameConfiguration"],
            'IdFrequency': data["IdFrequency"], 
            'Longitude': data["Longitude"],
            'Latitude': data["Latitude"],
            'DateTimeUpdate': datetime.utcnow(),
            'DateTimeActivation': formatted_datetime,  
            'IsActive': data["IsActive"],
            'IdMetric': data["IdMetric"] ,
            'Symbol': data["Symbol"], 
            'Value': data["Value"]
        }
        new_element_data=ConfigurationUserRepo.patch_element(data["IdConfiguration"], new_element_data)
        kf = True
        json_data = json.dumps(kf)

        #avviso notifier service che e´ stata modificata una configurazione
        new_element_data=ConfigurationUserRepo.get_new_configuration_for_today(new_element_data["Id"])
        headersRequest = KafkaHeader(IdOffsetResponse=-1,Type=MessageType.Request.value ,Tag="PatchConfiguration", Creator=Configurations().group_id, Code = MessageCode.Ok.value)
        ProducerClass.send_message(headersRequest.headers_list,Json.convert_object_to_json(new_element_data),GestoreDestinatari().determina_destinatario("SchedulerService"))        
        print(f'{str(headersRequest.to_string())}')
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - {str(headersRequest.to_string())} - {inspect.currentframe().f_globals['__file__']}")        
        return json_data

    def handle_tag_GetConfiguration( data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_GetConfiguration  - {inspect.currentframe().f_globals['__file__']}")
        return ConfigurationUserRepo.get_all_by_user(data["IdUser"])

    def handle_tag_DeleteConfiguration( data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_DeleteConfiguration  - {inspect.currentframe().f_globals['__file__']}")
        if data["IdUser"]==None or data["IdUser"]==0:
            raise Exception(str("Il campo IdUser non puo essere vuoto"))
            return
        if data["IdConfiguration"]==None or data["IdConfiguration"]==0:
            raise Exception(str("Il campo IdConfiguration non puo essere vuoto"))
            return
        res=ConfigurationUserRepo.delete_element(data["IdUser"], data["IdConfiguration"])
        kf = True
        json_data = json.dumps(kf)
        
        #avviso notifier service che è stata eliminata una configurazione
        headersRequest = KafkaHeader(IdOffsetResponse=-1,Type=MessageType.Request.value ,Tag="DeleteConfiguration", Creator=Configurations().group_id, Code = MessageCode.Ok.value)
        ProducerClass.send_message(headersRequest.headers_list,Json.convert_object_to_json(data["IdConfiguration"]),GestoreDestinatari().determina_destinatario("SchedulerService"))        
        print(f'{str(headersRequest.to_string())}')
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - {str(headersRequest.to_string())} - {inspect.currentframe().f_globals['__file__']}")
        return json_data

    def handle_tag_GetConfigurationForToday( data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_GetConfigurationForToday  - {inspect.currentframe().f_globals['__file__']}")
        return ConfigurationUserRepo.get_all_for_today()
    
    #METRIC
    def handle_tag_AddMetric( data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_AddMetric  - {inspect.currentframe().f_globals['__file__']}")
        result =MetricRepo.get_element_by_field(data["Field"])
        if result is None:
            raise Exception(str("La metrica non esiste nel database"))
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
        if data["Parent"]==None or data["Parent"]==0:
            raise Exception(str("Il campo Parent non puo essere vuoto"))
            return
        if data["Description"]==None or data["Description"]==0:
            raise Exception(str("Il campo Description non puo essere vuoto"))
            return
        new_element_data = {
            'Field': data["Field"], 
            'Type': data["Type"],
            'ValueUnit': data["ValueUnit"],
            'Parent': data["Parent"],
            'Description': data["Description"],
            'IsActive': data["IsActive"]
        }
        MetricRepo.add_element(new_element_data)
        kf = True
        json_data = json.dumps(kf)
        return json_data

    def handle_tag_PatchMetric( data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_PatchMetric  - {inspect.currentframe().f_globals['__file__']}")
        result =MetricRepo.get_element_by_field(data["Field"])
        if result is None:
            raise Exception(str("La metrica non esiste nel database"))
            return
        new_element_data = { 
            'Field': data["Field"],
            'Type': data["Type"],
            'ValueUnit': data["ValueUnit"],
            'Parent': data["Parent"],
            'Description': data["Description"],
            'IsActive': data["IsActive"]
        }
        MetricRepo.patch_element(data["IdMetric"], new_element_data)
        kf = True
        json_data = json.dumps(kf)
        return json_data

    def handle_tag_GetMetric( data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_GetMetric  - {inspect.currentframe().f_globals['__file__']}")
        return MetricRepo.get_all()

    def handle_tag_DeleteMetric( data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_DeleteMetric  - {inspect.currentframe().f_globals['__file__']}")
        if data["IdMetric"]==None or data["IdMetric"]==0:
            raise Exception(str("Il campo IdMetric non puo essere vuoto"))
            return
        MetricRepo.delete_element( data["IdMetric"])
        kf = True
        json_data = json.dumps(kf)
        return json_data
    
    #FREQUENCY
    def handle_tag_AddFrequency( data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_AddFrequency  - {inspect.currentframe().f_globals['__file__']}")
        result =FrequencyRepo.get_element_by_frequency_name(data["FrequencyName"])
        if result is None:
            raise Exception(str("La frequenza non esiste nel database"))
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
        kf = True
        json_data = json.dumps(kf)
        return json_data

    def handle_tag_PatchFrequency( data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_PatchFrequency  - {inspect.currentframe().f_globals['__file__']}")
        result =FrequencyRepo.get_element_by_frequency_name(data["FrequencyName"])
        if result is None:
            raise Exception(str("La frequenza non esiste nel database"))
            return
        new_element_data = {
            'FrequencyName': data["FrequencyName"], 
            'Minutes': data["Minutes"],
            'IsActive': data["IsActive"]
        }
        FrequencyRepo.patch_element(data["IdFrequency"], new_element_data)
        kf = True
        json_data = json.dumps(kf)
        return json_data

    def handle_tag_GetFrequency( data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_GetFrequency  - {inspect.currentframe().f_globals['__file__']}")
        return FrequencyRepo.get_all()

    def handle_tag_DeleteFrequency( data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_DeleteFrequency  - {inspect.currentframe().f_globals['__file__']}")
        if data["IdFrequency"]==None or data["IdFrequency"]==0:
            raise Exception(str("Il campo IdFrequency non puo essere vuoto"))
            return
        FrequencyRepo.delete_element( data["IdFrequency"])
        kf = True
        json_data = json.dumps(kf)
        return json_data  

    tag_handlers = {
    "AddConfiguration": handle_tag_AddConfiguration,
    "PatchConfiguration": handle_tag_PatchConfiguration,
    "GetConfiguration": handle_tag_GetConfiguration,
    "DeleteConfiguration": handle_tag_DeleteConfiguration,
    
    "GetConfigurationForToday": handle_tag_GetConfigurationForToday,
    
    "AddMetric": handle_tag_AddMetric,
    "PatchMetric": handle_tag_PatchMetric,
    "GetMetric": handle_tag_GetMetric,
    "DeleteMetric": handle_tag_DeleteMetric,
    
    "AddFrequency": handle_tag_AddFrequency,
    "PatchFrequency": handle_tag_PatchFrequency,
    "GetFrequency": handle_tag_GetFrequency,
    "DeleteFrequency": handle_tag_DeleteFrequency,
    }
    