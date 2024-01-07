from DB.Session import Session
from DB.Model import Frequency
from Utils.Logger import Logger
from datetime import datetime 
import inspect
class FrequencyRepo:
        
    def get_all():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            result_list =session.query(Frequency).all()
            result_dicts = []
            for result in result_list:
                result_dict = {
                    "Id": result.Id,
                    "Minutes":result.Minutes,
                    "FrequencyName": (result.FrequencyName) if result.FrequencyName is not None else None,
                    "IsActive": bool(result.IsActive) if result.IsActive is not None else None,
                }
                result_dicts.append(result_dict)
            return result_dicts
        
    def get_element(id_Frequency=None):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_element  - {inspect.currentframe().f_globals['__file__']}")
        if id_Frequency is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(Frequency)
            query = query.filter_by(Id=id_Frequency)
            return query.first() 
        
    def get_element_by_frequency_name(frequency_name=None):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_element_by_frequency_name  - {inspect.currentframe().f_globals['__file__']}")
        if frequency_name is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(Frequency)
            query = query.filter_by(FrequencyName=frequency_name)
            return query.first()  
    
    def add_element(new_element_data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_element  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            new_element = Frequency(**new_element_data)
            session.add(new_element)
            session.commit()
                
    def patch_element(element_id, patch_data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - patch_element  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            element_to_patch = session.merge(Frequency(Id=element_id))
            if element_to_patch:
                if 'Minutes' in patch_data and patch_data['Minutes'] is not None:
                    element_to_patch.Minutes = patch_data['Minutes']
                if 'FrequencyName' in patch_data and patch_data['FrequencyName'] is not None:
                    element_to_patch.FrequencyName = patch_data['FrequencyName']
                if 'IsActive' in patch_data and patch_data['IsActive'] is not None:
                    element_to_patch.IsActive = patch_data['IsActive']
                session.commit()
            
    def delete_element(id_Frequency=None):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - delete_element  - {inspect.currentframe().f_globals['__file__']}")
        if id_Frequency is None:
            return
        with Session.get_database_session() as session:
            query = session.query(Frequency)
            if id_Frequency is not None:
                query = query.filter_by(Id=id_Frequency)
            elements_to_delete = query.all()
            for element_to_delete in elements_to_delete:
                session.delete(element_to_delete)
            session.commit()