from datetime import datetime, timedelta
from DB.Session import Session
from DB.Model import   SlaMetricStatusView, Slametricstatus
from sqlalchemy import text
from Utils.Logger import Logger
import inspect
from datetime import datetime 

class SlaMetricStatusRepo:
        
    def get_all():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            resultList = session.query(SlaMetricStatusView).all()
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "IdSla": result.IdSla,
                    "Symbol": result.Symbol,
                    "Value": float(result.Value) if result.Value is not None else None,
                    "datetime": result.datetime.strftime('%Y-%m-%d %H:%M:%S') if result.datetime is not None else None,
                    "Metric": result.Metric,
                    "MetricDescription": result.MetricDescription,
                    "Code": result.Code,
                    "StatusDescription": result.StatusDescription,
                }
                result_dicts.append(result_dict)
            return result_dicts
    
    def patch_element(new_element_data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_element  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            existing_element = session.query(Slametricstatus).filter_by(
                IdSla=new_element_data.IdSla,
                Action=new_element_data.Action,
                Code=new_element_data.Code,
                Controller=new_element_data.Controller,
                Endpoint=new_element_data.Endpoint,
                Instance=new_element_data.Instance,
                Job=new_element_data.Job,
                Method=new_element_data.Method).first()
            if existing_element:
                existing_element.IdStatus= new_element_data.IdStatus
                existing_element.Datetime= new_element_data.Datetime
                merged_element = existing_element
            else:
                new_element = Slametricstatus(**vars(new_element_data))
                merged_element = session.merge(new_element)
            session.commit()
        return merged_element
            
