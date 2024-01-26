from DB.Session import Session
from DB.Model import MessageSent
from Utils.Logger import Logger
from datetime import datetime 
import inspect
class MessageSentRepo:
    
    def get_all():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            return session.query(MessageSent).all()
 
    def add_message(data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_message  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            new_message =data
            session.add(new_message)
            session.commit()
    
    def get_latest_message():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_latest_message  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            latest_message = session.query(MessageSent).order_by(MessageSent.id.desc()).first()
            return latest_message
