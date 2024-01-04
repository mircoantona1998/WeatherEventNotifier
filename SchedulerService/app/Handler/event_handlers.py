from datetime import datetime
from DB.Repository.ScheduleRepo import ScheduleRepo
from DB.Repository.ScheduleResponseRepo import ScheduleResponseRepo

class EventHandlers:    
    
    #CONFIGURATION CHANGE
    def handle_tag_AddConfiguration(data):
        #controlla se data attivazione della nuova configurazione è di oggi oppure no, se di oggi aggiunge schedulazione, altrimenti no
        if data["DateTimeActivation"]!=None and data["IsActive"]==True:
                datetime_obj = datetime.strptime(data["DateTimeActivation"], '%Y-%m-%d %H:%M:%S')
                if  datetime_obj <= datetime.utcnow():
                      new_element_data = {
                      'IdConfiguration': data["Id"],
                      'DateTimeToSchedule': data["DateTimeActivation"],
                      'ToWork': 1,
                      'Minutes':data["Minutes"],
                      'FieldMetric':data["Field"],
                      'Symbol':data["Symbol"],
                      'Value':data["Value"],
                      'IdUser':data["IdUser"],
                      'Latitude':data["Latitude"],
                      'Longitude':data["Longitude"],
                      'ParentMetric':data["Parent"],
                        }
                      ScheduleRepo.add_schedule(new_element_data)
                      return None
                else:
                    return None
        else:
            return None
   
    def handle_tag_PatchConfiguration(data):
        #ci interessa solo sapere se è modificata la data di attivazione o se è stata disattivata,altrimenti la cancelliamo dalle schedulazioni
         if data["DateTimeActivation"]!=None and data["IsActive"]==True:
                datetime_obj = datetime.strptime(data["DateTimeActivation"], '%Y-%m-%d %H:%M:%S')
                if  datetime_obj <= datetime.utcnow():
                    ScheduleRepo.delete_schedule(data["Id"]) 
                    new_element_data = {
                      'IdConfiguration': data["Id"],
                      'DateTimeToSchedule': data["DateTimeActivation"],
                      'ToWork': 1,
                      'Minutes':data["Minutes"],
                      'FieldMetric':data["Field"],
                      'Symbol':data["Symbol"],
                      'Value':data["Value"],
                      'IdUser':data["IdUser"],
                      'Latitude':data["Latitude"],
                      'Longitude':data["Longitude"],
                      'ParentMetric':data["Parent"],
                        }
                    ScheduleRepo.add_schedule(new_element_data)
         else:
             ScheduleRepo.delete_schedule(data["Id"]) 
         return None
    
    def handle_tag_DeleteConfiguration(data):
        ScheduleRepo.delete_schedule(data)
        return None

    def handle_tag_GetConfigurationForToday(dat):
        #controlla se data attivazione della nuova configurazione è di oggi oppure no, se di oggi aggiunge schedulazione, altrimenti no
        ScheduleRepo.delete_all_schedules()
        for data in dat["Data"]:
            if data["DateTimeActivation"]!=None and data["IsActive"]==True:
                    datetime_obj = datetime.strptime(data["DateTimeActivation"], '%Y-%m-%d %H:%M:%S')
                    if  datetime_obj <= datetime.utcnow():
                          new_element_data = {
                            'IdConfiguration': data["Id"],
                            'DateTimeToSchedule': data["DateTimeActivation"],
                            'ToWork': 1,
                            'Minutes':data["Minutes"],
                            'FieldMetric':data["Field"],
                            'Symbol':data["Symbol"],
                            'Value':data["Value"],
                            'IdUser':data["IdUser"],
                            'Latitude':data["Latitude"],
                            'Longitude':data["Longitude"],
                            'ParentMetric':data["Parent"],
                             }
                          ScheduleRepo.add_schedule(new_element_data)                         
                    else:
                        continue
            else:
                continue
        ScheduleResponseRepo.add_response_schedule()
        return None
    
    def handle_tag_SchedulationCurrentHour():
        return ScheduleRepo.get_all_current_hour()
    
    def handle_tag_GetSchedulation(data):
        return ScheduleRepo.get_all_by_user(data["IdUser"])

    tag_handlers = {    
    "AddConfiguration": handle_tag_AddConfiguration,
    "PatchConfiguration": handle_tag_PatchConfiguration,
    "DeleteConfiguration": handle_tag_DeleteConfiguration, 
    "GetConfigurationForToday": handle_tag_GetConfigurationForToday,
    "SchedulationCurrentHour": handle_tag_SchedulationCurrentHour,
    
     "GetSchedulation": handle_tag_GetSchedulation,
    }
