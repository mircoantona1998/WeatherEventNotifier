
from Utils.Telegram import Telegram


class EventHandlers:  
    
    def handle_tag_NewTip(dat):
        data=dat["Data"]
        result=Telegram().send_message("1914585178",data["Message"])
        if result["ok"]==True:
            print("inviato")
        return None
    tag_handlers = {    
       "NewTip": handle_tag_NewTip,
    }
