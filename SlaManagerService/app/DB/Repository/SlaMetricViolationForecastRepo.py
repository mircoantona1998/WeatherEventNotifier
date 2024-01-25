from datetime import datetime, timedelta
from DB.Session import Session
from DB.Model import   SlaMetricViolationForecastView, Slametricviolationforecast
from sqlalchemy import text
from Utils.Logger import Logger
import inspect
from datetime import datetime 
class SlaMetricViolationForecastRepo:
        
    def get_all():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            resultList = session.query(SlaMetricViolationForecastView).all()
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "IdSla": result.IdSla,
                    "Symbol": result.Symbol,
                    "DesiredValue": float(result.DesiredValue) if result.DesiredValue is not None else None,
                    "MisuredValue": float(result.MisuredValue) if result.MisuredValue is not None else None,                   
                    "Datetime": result.Datetime.strftime('%Y-%m-%d %H:%M:%S') if result.Datetime is not None else None,
                    "Metric": result.Metric,
                    "MetricDescription": result.MetricDescription,
                    "Violation": result.Violation,
                }
                result_dicts.append(result_dict)
            return result_dicts
    
    def add_element(new_element_data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_element  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            new_element = Slametricviolationforecast(**vars(new_element_data))
            session.add(new_element)
            session.commit()