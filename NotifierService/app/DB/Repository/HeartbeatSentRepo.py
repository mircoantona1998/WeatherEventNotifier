from Utils.Logger import Logger
import inspect
from datetime import datetime
from DB.Session import Session
from DB.Model import HeartbeatSent

class HeartbeatSentRepo:
        
    def get_last_element():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_last_element - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            last_element = session.query(HeartbeatSent).order_by(HeartbeatSent.datetime.desc()).first()   
            return last_element
    
    def add_heartbeat_sent():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_heartbeat_sent - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            current_date = datetime.utcnow()
            new_element = HeartbeatSent(datetime=current_date)
            session.add(new_element)
            session.commit()
            return    
        
    
