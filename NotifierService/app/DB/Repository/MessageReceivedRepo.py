from DB.Session import Session
from DB.Model import MessageReceived
from Utils.Logger import Logger
import inspect
from datetime import datetime
class MessageReceivedRepo:
    
    def get_all():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            return session.query(MessageReceived).all()
   
    def add_message(data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_message - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            new_message =data
            session.add(new_message)
            session.commit()
  
    def get_latest_message():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_latest_message - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            latest_message = session.query(MessageReceived).order_by(MessageReceived.id.desc()).first()
            return latest_message
