from Utils.Logger import Logger
import inspect
from datetime import datetime
from DB.Session import Session
from DB.Model import RequestSchedulation

class ScheduleRequestRepo:
        
    def get_last_element():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_last_element - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            last_element = session.query(RequestSchedulation).order_by(RequestSchedulation.date.desc()).first()   
            return last_element
    
    def add_request_schedule():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_request_schedule - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            current_date = datetime.utcnow().date()
            new_element = RequestSchedulation(date=current_date)
            session.add(new_element)
            session.commit()
            return    
        
    
