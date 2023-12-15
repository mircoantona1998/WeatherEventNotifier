from DB.Session import Session
from DB.Model import EventsConfiguration

class EventsConfigurationRepo:
        
    def get_all_by_user(user_id):
        with Session.get_database_session() as session:
            return session.query(EventsConfiguration).filter_by(IdUser=user_id).all()
        
    def add_element(new_element_data):
        with Session.get_database_session() as session:
            new_element = EventsConfiguration(**new_element_data)
            session.add(new_element)
            session.commit()
                
    def patch_element(element_id, patch_data):
        with Session.get_database_session() as session:
            element_to_patch = session.merge(EventsConfiguration(Id=element_id))
            if element_to_patch:
                if 'IdUser' in patch_data and patch_data['IdUser'] is not None:
                    element_to_patch.IdUser = patch_data['IdUser']
                if 'Longitude' in patch_data and patch_data['Longitude'] is not None:
                    element_to_patch.Longitude = patch_data['Longitude']
                if 'Latitude' in patch_data and patch_data['Latitude'] is not None:
                    element_to_patch.Latitude = patch_data['Latitude']
                if 'Metric' in patch_data and patch_data['Metric'] is not None:
                    element_to_patch.Metric = patch_data['Metric']
                if 'Frequency' in patch_data and patch_data['Frequency'] is not None:
                    element_to_patch.Frequency = patch_data['Frequency']
                if 'DateTimeUpdate' in patch_data and patch_data['DateTimeUpdate'] is not None:
                    element_to_patch.DateTimeUpdate = patch_data['DateTimeUpdate']
                session.commit()
            
    def delete_element(id_user=None, id_configuration=None):
        if id_user is None and id_configuration is None:
            return
        with Session.get_database_session() as session:
            query = session.query(EventsConfiguration)
            if id_user is not None:
                query = query.filter_by(IdUser=id_user)
            if id_configuration is not None:
                query = query.filter_by(Id=id_configuration)
            elements_to_delete = query.all()
            for element_to_delete in elements_to_delete:
                session.delete(element_to_delete)
            session.commit()