from Utils.Logger import Logger
import inspect
from datetime import datetime
from DBUsers.Repository.UsersRepo import UsersRepo
from DB.Repository.ScheduleRepo import ScheduleRepo
from DB.Repository.ScheduleResponseRepo import ScheduleResponseRepo

class EventHandlers:    
    
    #CONFIGURATION CHANGE
    def handle_tag_AddConfiguration(data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_AddConfiguration - {inspect.currentframe().f_globals['__file__']}")
        #controlla se data attivazione della nuova configurazione è di oggi oppure no, se di oggi aggiunge schedulazione, altrimenti no
        if data["DateTimeActivation"]!=None and data["IsActive"]==True:
                datetime_obj = datetime.strptime(data["DateTimeActivation"], '%Y-%m-%d %H:%M:%S')
                if  datetime_obj <= datetime.utcnow():
                      new_element_data = {
                      'IdConfiguration': data["Id"],
                      'IdMetric': data["IdMetric"],
                      'NameConfiguration': data["NameConfiguration"],
                      'DateTimeToSchedule': data["DateTimeActivation"],
                      'Minutes':data["Minutes"],
                      'FieldMetric':data["Field"],
                      'Symbol':data["Symbol"],
                      'Value':data["Value"],
                      'IdUser':data["IdUser"],
                      'Latitude':data["Latitude"],
                      'Longitude':data["Longitude"],
                      'ParentMetric':data["Parent"],
                      'ValueUnit':data["ValueUnit"],
                      'Description':data["Description"],
                        }
                      ScheduleRepo.add_schedule(new_element_data,datetime_obj)
                      return None
                else:
                    return None
        else:
            return None
   
    def handle_tag_PatchConfiguration(data):
         Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_PatchConfiguration - {inspect.currentframe().f_globals['__file__']}")
        #ci interessa solo sapere se è modificata la data di attivazione o se è stata disattivata,cosi la cancelliamo dalle schedulazioni e la facciamo ricreare
         if data["DateTimeActivation"]!=None and data["IsActive"]==True:
                datetime_obj = datetime.strptime(data["DateTimeActivation"], '%Y-%m-%d %H:%M:%S')
                if  datetime_obj <= datetime.utcnow():
                    ScheduleRepo.delete_schedule(data["Id"]) 
                    new_element_data = {
                      'IdConfiguration': data["Id"],
                      'IdMetric': data["IdMetric"],
                      'NameConfiguration': data["NameConfiguration"],
                      'DateTimeToSchedule': data["DateTimeActivation"],
                      'Minutes':data["Minutes"],
                      'FieldMetric':data["Field"],
                      'Symbol':data["Symbol"],
                      'Value':data["Value"],
                      'IdUser':data["IdUser"],
                      'Latitude':data["Latitude"],
                      'Longitude':data["Longitude"],
                      'ParentMetric':data["Parent"],
                      'ValueUnit':data["ValueUnit"],
                      'Description':data["Description"],
                        }
                    ScheduleRepo.add_schedule(new_element_data,datetime_obj)
         else:
             ScheduleRepo.delete_schedule(data["Id"]) 
         return None
    
    def handle_tag_DeleteConfiguration(data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_DeleteConfiguration - {inspect.currentframe().f_globals['__file__']}")
        ScheduleRepo.delete_schedule(data)
        return None

    def handle_tag_GetConfigurationForToday(dat):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_GetConfigurationForToday - {inspect.currentframe().f_globals['__file__']}")
        #controlla se data attivazione della nuova configurazione è di oggi oppure no, se di oggi aggiunge schedulazione, altrimenti no
        for data in dat["Data"]:
            lastSchedule = ScheduleRepo.get_element_last_datetime_to_schedule(data["Id"])
            ScheduleRepo.delete_schedule(data["Id"])
            if data["DateTimeActivation"]!=None and data["IsActive"]==True:
                    datetime_obj = datetime.strptime(data["DateTimeActivation"], '%Y-%m-%d %H:%M:%S')
                    if  datetime_obj <= datetime.utcnow():
                          new_element_data = {
                            'IdConfiguration': data["Id"],
                            'IdMetric': data["IdMetric"],
                            'NameConfiguration': data["NameConfiguration"],
                            'DateTimeToSchedule': data["DateTimeActivation"],
                            'Minutes':data["Minutes"],
                            'FieldMetric':data["Field"],
                            'Symbol':data["Symbol"],
                            'Value':data["Value"],
                            'IdUser':data["IdUser"],
                            'Latitude':data["Latitude"],
                            'Longitude':data["Longitude"],
                            'ParentMetric':data["Parent"],
                            'ValueUnit':data["ValueUnit"],
                            'Description':data["Description"],
                             }
                          ScheduleRepo.add_schedule(new_element_data,datetime_obj,lastSchedule)                         
                    else:
                        continue
            else:
                continue
        ScheduleResponseRepo.add_response_schedule()
        return None
    
    def handle_tag_UsersCurrent():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_UsersCurrent - {inspect.currentframe().f_globals['__file__']}")
        users=UsersRepo.get()
        result_dicts = []
        for user in users:
            result_dict = {
                'IdUser': user.Id,
                }
            result_dicts.append(result_dict)                        
        return result_dicts 
    
    def handle_tag_SchedulationCurrent():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_SchedulationCurrent- {inspect.currentframe().f_globals['__file__']}")
        return ScheduleRepo.get_all_current()
    
    def handle_tag_GetSchedulation(data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_GetSchedulation - {inspect.currentframe().f_globals['__file__']}")
        return ScheduleRepo.get_all_by_user(data["IdUser"])

    tag_handlers = {    
    "AddConfiguration": handle_tag_AddConfiguration,
    "PatchConfiguration": handle_tag_PatchConfiguration,
    "DeleteConfiguration": handle_tag_DeleteConfiguration, 
    "GetConfigurationForToday": handle_tag_GetConfigurationForToday,
    "SchedulationCurrent": handle_tag_SchedulationCurrent,
    
    "UsersCurrent": handle_tag_UsersCurrent,

     "GetSchedulation": handle_tag_GetSchedulation,
    }
