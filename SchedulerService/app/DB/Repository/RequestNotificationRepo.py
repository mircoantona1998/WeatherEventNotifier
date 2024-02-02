from Configurations.Configurations import Configurations
from Utils.Logger import Logger
import inspect
from datetime import datetime
from DB.Session import Session
from DB.Model import RequestNotification

class RequestNotificationRepo:
        
    def get_last_element():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_last_element - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            last_element = session.query(RequestNotification).filter_by(cluster=int(Configurations().cluster),partition=int(Configurations().partition)).order_by(RequestNotification.datetime.desc()).first()   
            return last_element
    def delete_schedule_request_notification():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - delete_schedule - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            query = session.query(RequestNotification)
            query = query.filter_by(cluster=int(Configurations().cluster),partition=int(Configurations().partition))
            elements_to_delete = query.all()
            for element_to_delete in elements_to_delete:
                session.delete(element_to_delete)
            session.commit()
            return True
    
    def add_request_notification():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_request_notification - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            current_date = datetime.utcnow().replace( second=0, microsecond=0)
            rounded_minute = (current_date.minute // 5) * 5
            current_date = current_date.replace(minute=rounded_minute)
            new_element = RequestNotification(datetime=current_date,cluster=int(Configurations().cluster),partition=int(Configurations().partition))
            session.add(new_element)
            session.commit()
            return    
        
    
