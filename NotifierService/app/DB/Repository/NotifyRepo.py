from DB.Session import Session
from DB.Model import Notify
from Utils.Logger import Logger
import inspect
from datetime import datetime
class NotifyRepo:
        
    def get_all():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            resultList = session.query(Notify).all()
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "Id": result.Id,
                    "IdUser": result.IdUser,
                    "IdSchedule": result.IdSchedule,
                    "Message": result.Message,
                    "DateTimeCreate": result.DateTimeCreate.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeCreate is not None else None,
                    "IdConfiguration": result.IdConfiguration,
                    "ValueWeather": float(result.ValueWeather) if result.ValueWeather is not None else None,
                }
                result_dicts.append(result_dict)
            return result_dicts
        
    def get_all_by_user(id_user=None):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all_by_user - {inspect.currentframe().f_globals['__file__']}")
        if id_user is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(Notify)
            query = query.filter_by(IdUser=id_user)
            query = query.order_by(Notify.DateTimeCreate.desc()) 
            resultList = query.all()
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "Id": result.Id,
                    "IdUser": result.IdUser,
                    "IdSchedule": result.IdSchedule,
                    "Message": result.Message,
                    "DateTimeCreate": result.DateTimeCreate.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeCreate is not None else None,
                    "IdConfiguration": result.IdConfiguration,
                    "ValueWeather": float(result.ValueWeather) if result.ValueWeather is not None else None,
                }
                result_dicts.append(result_dict)
            return result_dicts
        
    def add_element(new_element_data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_element - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            new_element = Notify(**new_element_data)
            session.add(new_element)
            session.commit()
            result_dict = {
                    "Id": new_element.Id,
                    "IdUser": new_element.IdUser,
                    "IdSchedule": new_element.IdSchedule,
                    "DateTimeCreate": new_element.DateTimeCreate.strftime('%Y-%m-%d %H:%M:%S') if new_element.DateTimeCreate is not None else None,                   
                    "Message": new_element.Message,
                    "IdConfiguration": new_element.IdConfiguration,
                    "ValueWeather": float(new_element.ValueWeather) if new_element.ValueWeather is not None else None,
                }
            return result_dict
