from DB.Session import Session
from DB.Model import TelegramMessages

class TelegramMessagesRepo:
    
    def get_all():
        with Session.get_database_session() as session:
            return session.query(TelegramMessages).all()
   
    def add_message(data):
        with Session.get_database_session() as session:
            new_message =data
            session.add(new_message)
            session.commit()
  
    def get_latest_message():
        with Session.get_database_session() as session:
            latest_message = session.query(TelegramMessages).order_by(TelegramMessages.id.desc()).first()
            return latest_message

