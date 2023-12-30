

from Utils.EmailService import EmailService


class EventHandlers:  
    
    def handle_tag_NewTip(dat):
        data=dat["Data"]
        EmailService("smtp.gmail.com",587,"mircoantona1998@gmail.com","xiwd dwiu hsgr gzvp").send_email("mircoantona1998@libero.it","Weather Event Notifier",data["Message"])
        return None
    tag_handlers = {    
       "NewTip": handle_tag_NewTip,
    }
