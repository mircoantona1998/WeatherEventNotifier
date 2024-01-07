from DB.Session import Session
from DB.Model import TelegramMessages
from Utils.Logger import Logger
import inspect
from datetime import datetime
class TelegramMessagesRepo:
    
    def get_all_by_user(user_id):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all_by_user - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            resultList = session.query(TelegramMessages).filter_by(IdUser=user_id).all()
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "Id": result.Id,
                    "IdUser": result.IdUser,
                    "IdChat": result.IdChat,
                    "Testo": result.Testo,
                    "Allegati": bool(result.Allegati) if result.Allegati is not None else None,
                    "DateCreate": result.DateCreate.strftime('%Y-%m-%d %H:%M:%S') if result.DateCreate is not None else None,
                    "WasSent": bool(result.WasSent) if result.WasSent is not None else None,
                    "Result": result.Result,
                }
                result_dicts.append(result_dict)
            return result_dicts

    def add_message(data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_message - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            new_element = TelegramMessages(**data)
            session.add(new_element)
            session.commit()


