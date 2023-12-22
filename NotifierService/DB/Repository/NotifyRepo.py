from DB.Session import Session
from DB.Model import Notify

class NotifyRepo:
        
    def get_all():
        with Session.get_database_session() as session:
            return session.query(Notify).all()
        
    def get_all_by_user(id_user=None):
        if id_user is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(Notify)
            query = query.filter_by(IdUser=id_user)
            return query.all() 
