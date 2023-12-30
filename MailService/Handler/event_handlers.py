

from Utils.EmailService import EmailService


class EventHandlers:  
    
    def handle_tag_NewTip(dat):
        EmailService("smtp.gmail.com",587,"mircoantona1998@gmail.com","xiwd dwiu hsgr gzvp").send_email("mircoantona1998@libero.it","weather event notifier","ciao prima email")
        return None
    tag_handlers = {    
       "NewTip": handle_tag_NewTip,
    }
