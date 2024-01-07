from DB.Session import Session
from DB.Model import TelegramConfiguration
from Utils.Logger import Logger
import inspect
from datetime import datetime
class TelegramConfigurationRepo:
    
    def get():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            query= session.query(TelegramConfiguration)
            result_list = query.all()  
            for result in result_list:
                result_dict = {
                    "id": result.Id,
                    "bot": result.bot if result.bot is not None else None,
                    "token":result.token if result.token is not None else None,
                }
                return result_dict
   
    def add_message(data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_message - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            new_message =data
            session.add(new_message)
            session.commit()
  
    def get_latest_message():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_latest_message - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            latest_message = session.query(TelegramConfiguration).order_by(TelegramConfiguration.id.desc()).first()
            return latest_message

