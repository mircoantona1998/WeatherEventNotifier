from datetime import datetime
from DB.Session import Session
from DB.Model import RequestNotification

class RequestNotificationRepo:
        
    def get_last_element():
        with Session.get_database_session() as session:
            last_element = session.query(RequestNotification).order_by(RequestNotification.datetime.desc()).first()   
            return last_element
    
    def add_request_notification():
        with Session.get_database_session() as session:
            current_date = datetime.now().replace(minute=0, second=0, microsecond=0)
            new_element = RequestNotification(datetime=current_date)
            session.add(new_element)
            session.commit()
            return    
        
    
