from datetime import datetime, timedelta
from DB.Session import Session
from DB.Model import   Metricdata
from sqlalchemy import text
from Utils.Logger import Logger
import inspect
from datetime import datetime 

class MetricDataRepo:
        
    def get_all():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            resultList = session.query(Metricdata).all()
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "Id": result.Id,
                    "Metric_name": result.Metric_name,
                    "Action": result.Action,
                    "Code": result.Code,
                    "Controller": result.Controller,
                    "Endpoint": result.Endpoint,
                    "Instance": result.Instance,
                    "Job": result.Job,
                    "Method": result.Method,
                    "Value1": float(result.Value1) if result.Value1 is not None else None,
                    "Value2": result.Method,                    
                }
                result_dicts.append(result_dict)
            return result_dicts
        
    def add_element(new_element_data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_element  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            new_element = Metricdata(**vars(new_element_data))
            session.add(new_element)
            session.commit()
   