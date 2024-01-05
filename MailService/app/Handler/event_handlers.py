from datetime import datetime
import json
from DB.Repository.MailConfigurationRepo import MailConfigurationRepo
from DB.Repository.MailRepo import MailRepo
from DB.Repository.MailUsersRepo import MailUsersRepo
from Utils.EmailService import EmailService


class EventHandlers:  
    
    def handle_tag_NewTip(dat):
        data=dat["Data"]
        user=MailUsersRepo.get_user_mail(data["IdUser"]) 
        if user!= None:
            config=MailConfigurationRepo.get() 
            result=EmailService("smtp.gmail.com",587,config["mail"],config["password"]).send_email(user["mail"],"Weather Event Notifier",data["Message"])
            mess=""
            if result ==True:
                mess="Ok"
            else:
                mess="Error"
            new_element_data = {
                'IdUser' :  data["IdUser"],
                'Mittente' :  config["mail"],
                'Destinatario' : user["mail"],
                'Oggetto' : "Weather Event Notifier",
                'Testo' : data["Message"],
                'Allegati' : False,
                'DateCreate' : datetime.utcnow(), 
                'WasSent':  result, 
                'Result' :  mess 
            }
            MailRepo.add_message(new_element_data)
        return None
    
    def handle_tag_GetMailSent(data):
        return MailRepo.get_all_by_user(data["IdUser"])
    
    def handle_tag_GetUserMail(data):
        return MailUsersRepo.get_user_mail(data["IdUser"])
    
    def handle_tag_AddUserMail(data):
        if data["IdUser"]==None or data["IdUser"]==0:
            raise Exception(str("Il campo idUser non puo essere vuoto"))
            return
        if data["Mail"]==None or data["Mail"]==0:
            raise Exception(str("Il campo Mail non puo essere vuoto"))
            return
        new_element_data = {
            'idUser': data["IdUser"], 
            'mail': data["Mail"],
            'isActive': True
        }
        MailUsersRepo.add_user_mail(new_element_data)
        kf = True
        json_data = json.dumps(kf)
        return json_data    
    
    def handle_tag_PatchUserMail(data):
        if data["IdUser"]==None or data["IdUser"]==0:
            raise Exception(str("Il campo idUser non puo essere vuoto"))
            return
        if data["Mail"]==None or data["Mail"]==0:
            raise Exception(str("Il campo Mail non puo essere vuoto"))
            return
        new_element_data = {
            'mail': data["Mail"],
            'isActive': data["IsActive"]
        }
        MailUsersRepo.patch_user_mail(data["IdUser"],new_element_data)
        kf = True
        json_data = json.dumps(kf)
        return json_data 
    
    def handle_tag_DeleteUserMail(data):
        if data["IdUser"]==None or data["IdUser"]==0:
            raise Exception(str("Il campo IdUser non puo essere vuoto"))
            return
        MailUsersRepo.delete_element( data["IdUser"])
        kf = True
        json_data = json.dumps(kf)
        return json_data
    
    tag_handlers = {    
       "NewTip": handle_tag_NewTip,
       
       "GetMailSent": handle_tag_GetMailSent,
       
       "GetUserMail": handle_tag_GetUserMail,
       "AddUserMail": handle_tag_AddUserMail,
       "PatchUserMail": handle_tag_PatchUserMail,
       "DeleteUserMail": handle_tag_DeleteUserMail,
    }
