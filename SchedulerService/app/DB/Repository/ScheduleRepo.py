from datetime import datetime, timedelta
from DB.Session import Session
from DB.Model import Schedule
from sqlalchemy import  func, cast, DateTime,desc
from Utils.Logger import Logger
import inspect
class ScheduleRepo:
        
    def get_all_by_user(user_id):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all_by_user - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            resultList = session.query(Schedule).filter_by(IdUser=user_id).all()
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "Id": result.Id,
                    "NameConfiguration": result.NameConfiguration,
                    "IdConfiguration": result.IdConfiguration,
                    "DateTimeToSchedule": result.DateTimeToSchedule.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeToSchedule is not None else None,
                    "FieldMetric":result.FieldMetric,
                    "Symbol":result.Symbol,
                    "Value":result.Value,
                    "IdUser":result.IdUser,
                    "Latitude":result.Latitude,
                    "Longitude":result.Longitude,
                    "ParentMetric":result.ParentMetric,
                    "ValueUnit":result.ValueUnit,
                    "Description":result.Description,
                   }
                result_dicts.append(result_dict)
            return result_dicts
        
    def get_element(id_user=None):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_element - {inspect.currentframe().f_globals['__file__']}")
        if id_user is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(Schedule)
            query = query.filter_by(IdUser=id_user)
            return query.first() 
        
    def get_element_last_datetime_to_schedule(id_configuration=None):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_element_last_datetime_to_schedule - {inspect.currentframe().f_globals['__file__']}")
        if id_configuration is None:
            return None  
        data_ieri = datetime.utcnow() - timedelta(days=1)
        data_ieri_inizio = datetime(data_ieri.year, data_ieri.month, data_ieri.day, 0, 0, 0)
        data_ieri_fine = datetime(data_ieri.year, data_ieri.month, data_ieri.day, 23, 59, 59)
        with Session.get_database_session() as session:
            query = session.query(Schedule)
            query = query.filter_by(IdConfiguration=id_configuration)
            query = query.filter(Schedule.DateTimeToSchedule >= data_ieri_inizio, Schedule.DateTimeToSchedule <= data_ieri_fine)
            query = query.order_by(desc(Schedule.DateTimeToSchedule))
            return query.first()
        
    def add_schedule(new_element_data, datetimeActivation, lastschedule=None,):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_schedule - {inspect.currentframe().f_globals['__file__']}")
        if  lastschedule is not None and lastschedule.DateTimeToSchedule is not None:
            new_element_data['DateTimeToSchedule'] = lastschedule.DateTimeToSchedule.replace( second=0, microsecond=0)
        else:
            data_ieri = datetime.utcnow() - timedelta(days=1)
            new_element_data['DateTimeToSchedule'] = datetime(data_ieri.year, data_ieri.month, data_ieri.day, datetimeActivation.hour, 0, 0)
        minutes_freq=new_element_data["Minutes"]
        del new_element_data["Minutes"]
        with Session.get_database_session() as session:
            while new_element_data['DateTimeToSchedule'] <= datetime.utcnow().replace(hour=23, minute=59, second=59, microsecond=0):  # Continua fino a mezzanotte
                new_element_data['DateTimeToSchedule'] = new_element_data['DateTimeToSchedule'] + timedelta(minutes=minutes_freq) 
                if new_element_data['DateTimeToSchedule'].day==datetime.utcnow().day and new_element_data['DateTimeToSchedule'] <= datetime.utcnow().replace(hour=23, minute=59, second=59, microsecond=0):
                    new_element = Schedule(**new_element_data)
                    session.add(new_element)
                    session.commit()
            return 
                      
    def delete_schedule(id_configuration):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - delete_schedule - {inspect.currentframe().f_globals['__file__']}")
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
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - delete_all_schedules - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            session.query(Schedule).delete()
            session.commit()
            
    def get_all_current():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all_current - {inspect.currentframe().f_globals['__file__']}")
        current_datetime = datetime.utcnow().replace( second=0, microsecond=0)
        rounded_minute = (current_datetime.minute // 5) * 5
        current_datetime = current_datetime.replace(minute=rounded_minute)
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
                    "NameConfiguration": result.NameConfiguration,
                    "IdConfiguration": result.IdConfiguration,
                    "DateTimeToSchedule": result.DateTimeToSchedule.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeToSchedule is not None else None,
                    "FieldMetric":result.FieldMetric,
                    "Symbol":result.Symbol,
                    "Value":result.Value,
                    "IdUser":result.IdUser,
                    "Latitude":result.Latitude,
                    "Longitude":result.Longitude,
                    "ParentMetric":result.ParentMetric,
                    "ValueUnit":result.ValueUnit,
                    "Description":result.Description,
                }
                result_dicts.append(result_dict)
            return result_dicts
        