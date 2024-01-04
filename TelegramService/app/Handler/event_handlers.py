import json
from DB.Repository.TelegramConfigurationRepo import TelegramConfigurationRepo
from DB.Repository.TelegramMessagesRepo import TelegramMessagesRepo
from DB.Repository.TelegramUsersRepo import TelegramUsersRepo
from Utils.Telegram import Telegram


class EventHandlers:  
    
    def handle_tag_NewTip(dat):
        data=dat["Data"]
        user=TelegramUsersRepo.get_user_telegram(data["IdUser"]) #aggiungere condizione se user presente o no
        config=TelegramConfigurationRepo.get_all() #creare get first   
        result=Telegram().send_message(user.chat_id,data["Message"],config.token)
        if result["ok"]==True:    
            new_element_data = {
                'IdUser' :  data["Mail"],
                'IdChat' :  user.chat_id,
                'Testo' : data["Message"],
                'Allegati' : False,
                'DateCreate' : "", #sistemare
                'WasSent':  True, 
                'Result' :  "Ok" 
            }
            TelegramMessagesRepo.add_message(new_element_data)
        else:
            new_element_data = {
                'IdUser' :  data["Mail"],
                'IdChat' :  user.chat_id,
                'Testo' : data["Message"],
                'Allegati' : False,
                'DateCreate' : "", #sistemare
                'WasSent':  False, 
                'Result' :  "ERRORE" 
            }
            TelegramMessagesRepo.add_message(new_element_data)
        return None
    
    def handle_tag_GetTelegramSent(data):
        return TelegramMessagesRepo.get_all_by_user(data["IdUser"])
    
    def handle_tag_GetUserTelegram(data):
        return TelegramUsersRepo.get_user_telegram(data["IdUser"])
    
    def handle_tag_AddUserTelegram(data):
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
