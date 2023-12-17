from DB.Session import Session
from DB.Model import Metric

class MetricRepo:
        
    def get_all():
        with Session.get_database_session() as session:
            return session.query(Metric).all()
        
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