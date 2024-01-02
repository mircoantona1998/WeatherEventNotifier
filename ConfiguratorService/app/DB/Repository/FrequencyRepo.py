from DB.Session import Session
from DB.Model import Frequency

class FrequencyRepo:
        
    def get_all():
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
        if id_Frequency is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(Frequency)
            query = query.filter_by(Id=id_Frequency)
            return query.first() 
        
    def get_element_by_frequency_name(frequency_name=None):
        if frequency_name is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(Frequency)
            query = query.filter_by(FrequencyName=frequency_name)
            return query.first()  
    
    def add_element(new_element_data):
        with Session.get_database_session() as session:
            new_element = Frequency(**new_element_data)
            session.add(new_element)
            session.commit()
                
    def patch_element(element_id, patch_data):
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