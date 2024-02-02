from Configurations.Configurations import Configurations
from Utils.Logger import Logger
import inspect
from datetime import datetime
from DB.Session import Session
from DB.Model import RequestSchedulation

class ScheduleRequestRepo:
        
    def get_last_element():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_last_element - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            last_element = session.query(RequestSchedulation).filter_by(cluster=int(Configurations().cluster),partition=int(Configurations().partition)).order_by(RequestSchedulation.date.desc()).first()   
            return last_element
        
    def delete_schedule_request():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - delete_schedule - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            query = session.query(RequestSchedulation)
            query = query.filter_by(cluster=int(Configurations().cluster),partition=int(Configurations().partition))
            elements_to_delete = query.all()
            for element_to_delete in elements_to_delete:
                session.delete(element_to_delete)
            session.commit()
            return True
    def add_request_schedule():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_request_schedule - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            current_date = datetime.utcnow().date()
            new_element = RequestSchedulation(date=current_date,cluster=int(Configurations().cluster),partition=int(Configurations().partition))
            session.add(new_element)
            session.commit()
            return    
        
    
