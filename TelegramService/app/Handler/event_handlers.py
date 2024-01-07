from Utils.Logger import Logger
import inspect
from datetime import datetime
import json
from DB.Repository.TelegramConfigurationRepo import TelegramConfigurationRepo
from DB.Repository.TelegramMessagesRepo import TelegramMessagesRepo
from DB.Repository.TelegramUsersRepo import TelegramUsersRepo
from Utils.Telegram import Telegram

class EventHandlers:  
    
    def handle_tag_NewTip(dat):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_NewTip - {inspect.currentframe().f_globals['__file__']}")
        data=dat["Data"]
        user=TelegramUsersRepo.get_user_telegram(data["IdUser"]) 
        if user != None:
            config=TelegramConfigurationRepo.get() 
            result=Telegram().send_message(user["ChatId"],data["Message"],config["token"])
            if result["ok"]==True:    
                new_element_data = {
                    'IdUser' :  data["IdUser"],
                    'IdChat' :  user["ChatId"],
                    'Testo' : data["Message"],
                    'Allegati' : False,
                    'DateCreate' : datetime.utcnow(), 
                    'WasSent':  True, 
                    'Result' :  "Ok" 
                }
                TelegramMessagesRepo.add_message(new_element_data)
            else:
                new_element_data = {
                    'IdUser' :  data["IdUser"],
                    'IdChat' :  user["ChatId"],
                    'Testo' : data["Message"],
                    'Allegati' : False,
                    'DateCreate' : datetime.utcnow(), 
                    'WasSent':  False, 
                    'Result' :  "ERRORE" 
                }
                TelegramMessagesRepo.add_message(new_element_data)
        return None
    
    def handle_tag_GetTelegramSent(data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_GetTelegramSent - {inspect.currentframe().f_globals['__file__']}")
        return TelegramMessagesRepo.get_all_by_user(data["IdUser"])
    
    def handle_tag_GetUserTelegram(data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_GetUserTelegram - {inspect.currentframe().f_globals['__file__']}")
        return TelegramUsersRepo.get_user(data["IdUser"])
    
    def handle_tag_AddUserTelegram(data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_AddUserTelegram - {inspect.currentframe().f_globals['__file__']}")
        if data["IdUser"]==None or data["IdUser"]==0:
            raise Exception(str("Il campo idUser non puo essere vuoto"))
            return
        if data["ChatId"]==None or data["ChatId"]==0:
            raise Exception(str("Il campo chat_id non puo essere vuoto"))
            return
        new_element_data = {
            'idUser': data["IdUser"], 
            'chat_id': data["ChatId"],
            'isActive': True
        }
        TelegramUsersRepo.add_user_telegram(new_element_data)
        kf = True
        json_data = json.dumps(kf)
        return json_data 
    
    def handle_tag_PatchUserTelegram(data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_PatchUserTelegram - {inspect.currentframe().f_globals['__file__']}")
        if data["IdUser"]==None or data["IdUser"]==0:
            raise Exception(str("Il campo idUser non puo essere vuoto"))
            return
        if data["ChatId"]==None or data["ChatId"]==0:
            raise Exception(str("Il campo chat_id non puo essere vuoto"))
            return
        new_element_data = {
            'chat_id': data["ChatId"],
            'isActive': data["IsActive"]
        }
        TelegramUsersRepo.patch_user_telegram(data["IdUser"],new_element_data)
        kf = True
        json_data = json.dumps(kf)
        return json_data 
    
    def handle_tag_DeleteUserTelegram(data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - handle_tag_DeleteUserTelegram - {inspect.currentframe().f_globals['__file__']}")
        if data["IdUser"]==None or data["IdUser"]==0:
            raise Exception(str("Il campo IdUser non puo essere vuoto"))
            return
        TelegramUsersRepo.delete_element( data["IdUser"])
        kf = True
        json_data = json.dumps(kf)
        return json_data
    
    
    
    tag_handlers = {    
       "NewTip": handle_tag_NewTip,
       
       "GetTelegramSent": handle_tag_GetTelegramSent,
       
       "GetUserTelegram": handle_tag_GetUserTelegram,
       "AddUserTelegram": handle_tag_AddUserTelegram,
       "PatchUserTelegram": handle_tag_PatchUserTelegram,
       "DeleteUserTelegram": handle_tag_DeleteUserTelegram,
    }
