from email import utils
import json
from datetime import datetime
from DB.Repository.NotifyRepo import NotifyRepo
from Utils.EnumMessageType import MessageType

class EventHandlers:    
    #NOTIFY
    def handle_tag_GetNotify(data):
           return NotifyRepo.get_all_by_user(data["IdUser"])
    
   
    tag_handlers = {
    "GetNotify": handle_tag_GetNotify,
    
    }