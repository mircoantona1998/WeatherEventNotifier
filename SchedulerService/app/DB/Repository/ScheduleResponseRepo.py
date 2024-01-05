from datetime import datetime
from DB.Session import Session
from DB.Model import ResponseSchedulation

class ScheduleResponseRepo:
        
    def get_last_element():
        with Session.get_database_session() as session:
            last_element = session.query(ResponseSchedulation).order_by(ResponseSchedulation.date.desc()).first()   
            return last_element
    
    def add_response_schedule():
        with Session.get_database_session() as session:
            current_date = datetime.utcnow().date()
            new_element = ResponseSchedulation(date=current_date)
            session.add(new_element)
            session.commit()
            return    
        
    
