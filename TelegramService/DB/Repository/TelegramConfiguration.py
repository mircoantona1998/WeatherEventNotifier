from DB.Session import Session
from DB.Model import TelegramConfiguration

class TelegramConfigurationRepo:
    
    def get_all():
        with Session.get_database_session() as session:
            return session.query(TelegramConfiguration).all()
   
    def add_message(data):
        with Session.get_database_session() as session:
            new_message =data
            session.add(new_message)
            session.commit()
  
    def get_latest_message():
        with Session.get_database_session() as session:
            latest_message = session.query(TelegramConfiguration).order_by(TelegramConfiguration.id.desc()).first()
            return latest_message

