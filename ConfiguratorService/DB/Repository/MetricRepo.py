from DB.Session import Session
from DB.Model import Metric

class MetricRepo:
        
    def get_all():
        with Session.get_database_session() as session:
            result_list =session.query(Metric).all()
            result_dicts = []
            for result in result_list:
                result_dict = {
                    "Id": result.Id,
                    "Field": result.Field if result.Field is not None else None,
                    "ValueUnit": result.ValueUnit if result.ValueUnit is not None else None,
                    "Type": result.Type if result.Type is not None else None,
                    "Parent": result.Parent if result.Parent is not None else None,
                    "Description": result.Description if result.Description is not None else None,
                    "IsActive": bool(result.IsActive) if result.IsActive is not None else None,
                }
                result_dicts.append(result_dict)
            return result_dicts
        
    def get_element(id_Metric=None):
        if id_Metric is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(Metric)
            query = query.filter_by(Id=id_Metric)
            return query.first()        
        
    def get_element_by_field(field_Metric=None):
        if field_Metric is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(Metric)
            query = query.filter_by(Field=field_Metric)
            return query.first()         
        
    def add_element(new_element_data):
        with Session.get_database_session() as session:
            new_element = Metric(**new_element_data)
            session.add(new_element)
            session.commit()
                
    def patch_element(element_id, patch_data):
        with Session.get_database_session() as session:
            element_to_patch = session.merge(Metric(Id=element_id))
            if element_to_patch:
                if 'Field' in patch_data and patch_data['Field'] is not None:
                    element_to_patch.Field = patch_data['Field']
                if 'ValueUnit' in patch_data and patch_data['ValueUnit'] is not None:
                    element_to_patch.ValueUnit = patch_data['ValueUnit']
                if 'Type' in patch_data and patch_data['Type'] is not None:
                    element_to_patch.Type = patch_data['Type']
                if 'Parent' in patch_data and patch_data['Parent'] is not None:
                    element_to_patch.Parent = patch_data['Parent']
                if 'Description' in patch_data and patch_data['Description'] is not None:
                    element_to_patch.Description = patch_data['Description']
                if 'Frequency' in patch_data and patch_data['Frequency'] is not None:
                    element_to_patch.Frequency = patch_data['Frequency']
                if 'IsActive' in patch_data and patch_data['IsActive'] is not None:
                    element_to_patch.IsActive = patch_data['IsActive']
                session.commit()
                
    def delete_element( id_Metric=None):
        if id_Metric is None:
            return
        with Session.get_database_session() as session:
            query = session.query(Metric)
            if id_Metric is not None:
                query = query.filter_by(Id=id_Metric)
            elements_to_delete = query.all()
            for element_to_delete in elements_to_delete:
                session.delete(element_to_delete)
            session.commit()