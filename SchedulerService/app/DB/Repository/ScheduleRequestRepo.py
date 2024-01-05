from datetime import datetime
from DB.Session import Session
from DB.Model import RequestSchedulation

class ScheduleRequestRepo:
        
    def get_last_element():
        with Session.get_database_session() as session:
            last_element = session.query(RequestSchedulation).order_by(RequestSchedulation.date.desc()).first()   
            return last_element
    
    def add_request_schedule():
        with Session.get_database_session() as session:
            current_date = datetime.utcnow().date()
            new_element = RequestSchedulation(date=current_date)
            session.add(new_element)
            session.commit()
            return    
        
    
