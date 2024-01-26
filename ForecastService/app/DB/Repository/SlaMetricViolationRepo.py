from datetime import datetime,timedelta
from DB.Session import Session
from DB.Model import    Slametricviolation
from Utils.Logger import Logger
import inspect
from datetime import datetime 
class SlaMetricViolationRepo:
        
    def get_all():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            resultList = session.query(Slametricviolation).all()
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "IdSla": result.IdSla,
                    "FromDesiredValue": float(result.FromDesiredValue) if result.FromDesiredValue is not None else None,
                    "ToDesiredValue": float(result.ToDesiredValue) if result.ToDesiredValue is not None else None,            
                    "MisuredValue": float(result.MisuredValue) if result.MisuredValue is not None else None,
                    "Datetime": result.Datetime.strftime('%Y-%m-%d %H:%M:%S') if result.Datetime is not None else None,
                    "Metric": result.Metric,
                    "MetricDescription": result.MetricDescription,
                    "Violation": result.Violation,
                }
                result_dicts.append(result_dict)
            return result_dicts
        
    def get_all_by_idSla_minutes(idSla, minutes):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            current_time = datetime.utcnow()
            max_datetime = current_time - timedelta(minutes=minutes)
            if idSla is not None:
                resultList = session.query(Slametricviolation)\
                    .filter_by(IdSla=idSla)\
                    .filter(Slametricviolation.Datetime >= max_datetime)\
                    .all()
            else: 
                     resultList = session.query(Slametricviolation)\
                    .filter(Slametricviolation.Datetime >= max_datetime)\
                    .all()
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "IdSla": result.IdSla,
                    "FromDesiredValue": float(result.FromDesiredValue) if result.FromDesiredValue is not None else None,
                    "ToDesiredValue": float(result.ToDesiredValue) if result.ToDesiredValue is not None else None,
                    "MisuredValue": float(result.MisuredValue) if result.MisuredValue is not None else None,
                    "Datetime": (result.Datetime.replace(second=0, microsecond=0) if result.Datetime is not None else None).strftime('%Y-%m-%d %H:%M:%S'),
                    "Metric": result.Metric,
                    "MetricDescription": result.MetricDescription,
                    "Violation": result.Violation,
                }
                result_dicts.append(result_dict)
            return result_dicts
    def add_element(new_element_data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_element  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            new_element = Slametricviolation(**vars(new_element_data))
            session.add(new_element)
            session.commit()