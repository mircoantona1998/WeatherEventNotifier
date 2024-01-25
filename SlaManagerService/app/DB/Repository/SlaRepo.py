from datetime import datetime, timedelta
from DB.Session import Session
from DB.Model import   SlaView
from sqlalchemy import text
from Utils.Logger import Logger
import inspect
from datetime import datetime 
class SlaRepo:
        
    def get_all():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            resultList = session.query(SlaView).all()
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "Id": result.Id,
                    "IdMonitoringMetric": int(result.IdMonitoringMetric) if result.IdMonitoringMetric is not None else None,
                    "Partition": int(result.Partition) if result.Partition is not None else None,
                    "Symbol": result.Symbol,
                    "Value": float(result.Value) if result.Value is not None else None,
                    "UpdateDatetime": result.UpdateDatetime.strftime('%Y-%m-%d %H:%M:%S') if result.UpdateDatetime is not None else None,
                    "Metric": result.Metric,
                    "Description": result.Description,
                }
                result_dicts.append(result_dict)
            return result_dicts
        
    def get_by_id_metric(Id):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            result = session.query(SlaView).filter_by(Id=Id).first()
            if result is not None:
                result_dict = {
                    "Id": result.Id,
                    "IdMonitoringMetric": int(result.IdMonitoringMetric) if result.IdMonitoringMetric is not None else None,
                    "Partition": int(result.Partition) if result.Partition is not None else None,
                    "Symbol": result.Symbol,
                    "Value": float(result.Value) if result.Value is not None else None,
                    "UpdateDatetime": result.UpdateDatetime.strftime('%Y-%m-%d %H:%M:%S') if result.UpdateDatetime is not None else None,
                    "Metric": result.Metric,
                    "Description": result.Description,
                }
                return result_dict
            return None
    
   