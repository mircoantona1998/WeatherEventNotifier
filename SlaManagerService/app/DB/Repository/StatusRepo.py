from datetime import datetime, timedelta
from DB.Session import Session
from DB.Model import   Status
from sqlalchemy import text
from Utils.Logger import Logger
import inspect
from datetime import datetime 
class StatusRepo:
        
    def get_all():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            resultList = session.query(Status).all()
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "Id": result.Id,
                    "Code": result.Code,
                    "Description": result.Description,
                }
                result_dicts.append(result_dict)
            return result_dicts

    def get_by_code(code):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            result = session.query(Status).filter_by(Code=code).first()
            result_dict = {
                "Id": result.Id,
                "Code": result.Code,
                "Description": result.Description,
            }
            return result_dict
   