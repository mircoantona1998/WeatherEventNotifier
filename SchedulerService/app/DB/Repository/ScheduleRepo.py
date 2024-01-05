from datetime import datetime, timedelta
from DB.Session import Session
from DB.Model import Schedule
from sqlalchemy import  func, cast, DateTime

class ScheduleRepo:
        
    def get_all_by_user(user_id):
        with Session.get_database_session() as session:
            resultList = session.query(Schedule).filter_by(IdUser=user_id).all()
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "Id": result.Id,
                    "ConfigurationName": result.ConfigurationName,
                    "IdConfiguration": result.IdConfiguration,
                    "DateTimeToSchedule": result.DateTimeToSchedule.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeToSchedule is not None else None,
                    "FieldMetric":result.FieldMetric,
                    "Symbol":result.Symbol,
                    "Value":result.Value,
                    "IdUser":result.IdUser,
                    "Latitude":result.Latitude,
                    "Longitude":result.Longitude,
                    "ParentMetric":result.ParentMetric,
                   }
                result_dicts.append(result_dict)
            return result_dicts
        
    def get_element(id_user=None):
        if id_user is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(Schedule)
            query = query.filter_by(IdUser=id_user)
            return query.first() 
        
    def add_schedule(new_element_data):
        minutes_freq=new_element_data["Minutes"]
        del new_element_data["Minutes"]
        with Session.get_database_session() as session:
            current_datetime = datetime.utcnow().replace(minute=0, second=0, microsecond=0)
            while current_datetime <= datetime.utcnow().replace(hour=23, minute=59, second=59, microsecond=0):  # Continua fino a mezzanotte
                new_element_data['DateTimeToSchedule'] = current_datetime + timedelta(minutes=minutes_freq)       
                new_element = Schedule(**new_element_data)
                session.add(new_element)
                session.commit()
                current_datetime += timedelta(minutes=minutes_freq)
            return 
                      
    def delete_schedule(id_configuration):
        if id_configuration is None:
            return False
        with Session.get_database_session() as session:
            query = session.query(Schedule)
            if id_configuration is not None:
                query = query.filter_by(IdConfiguration=id_configuration)
            elements_to_delete = query.all()
            for element_to_delete in elements_to_delete:
                session.delete(element_to_delete)
            session.commit()
            return True

    def delete_all_schedules():
        with Session.get_database_session() as session:
            session.query(Schedule).delete()
            session.commit()
            
    def get_all_current_hour():
        current_datetime = datetime.utcnow().replace(minute=0, second=0, microsecond=0)
        with Session.get_database_session() as session:
            resultList = (
                    session.query(Schedule)
                    .filter(cast(Schedule.DateTimeToSchedule, DateTime) == current_datetime)
                    .all()
                )
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "Id": result.Id,
                    "ConfigurationName": result.ConfigurationName,
                    "IdConfiguration": result.IdConfiguration,
                    "DateTimeToSchedule": result.DateTimeToSchedule.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeToSchedule is not None else None,
                    "FieldMetric":result.FieldMetric,
                    "Symbol":result.Symbol,
                    "Value":result.Value,
                    "IdUser":result.IdUser,
                    "Latitude":result.Latitude,
                    "Longitude":result.Longitude,
                    "ParentMetric":result.ParentMetric,
                }
                result_dicts.append(result_dict)
            return result_dicts
        