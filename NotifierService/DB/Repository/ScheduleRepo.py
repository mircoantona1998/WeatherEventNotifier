from DB.Session import Session
from DB.Model import Schedule

class ScheduleRepo:
        
    def get_all():
        with Session.get_database_session() as session:
            return session.query(Schedule).all()
        
    def get_element(id_user=None):
        if id_user is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(Schedule)
            query = query.filter_by(IdUser=id_user)
            return query.first() 
        
    def add_schedule(new_element_data):
        with Session.get_database_session() as session:
            new_element =Schedule(**new_element_data)
            session.add(new_element)
            session.commit()
            return new_element.as_dict()
                    
    def patch_schedule(element_id,patch_data):
        with Session.get_database_session() as session:
            element_to_patch = session.query(Schedule).filter_by(IdConfiguration=element_id).first()
            if element_to_patch:
                if 'Id' in patch_data and patch_data['Id'] is not None:
                    element_to_patch.IdConfiguration = patch_data['Id']
                if 'ToWork' in patch_data and patch_data['ToWork'] is not None:
                    element_to_patch.ToWork = patch_data['ToWork']
                session.commit()
                return element_to_patch.as_dict()
        
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
