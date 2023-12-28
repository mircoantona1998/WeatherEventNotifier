from DB.Session import Session
from DB.Model import Notify

class NotifyRepo:
        
    def get_all():
        with Session.get_database_session() as session:
            resultList = session.query(Notify).all()
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "Id": result.Id,
                    "IdUser": result.IdUser,
                    "IdSchedule": result.IdSchedule,
                    "Message": result.Message,
                    "DateTimeCreate": result.DateTimeCreate.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeCreate is not None else None,
                    "IsActive": bool(result.IsActive) if result.IsActive is not None else None,
                    "ToWork": bool(result.ToWork) if result.ToWork is not None else None,
                }
                result_dicts.append(result_dict)
            return result_dicts
        
    def get_all_by_user(id_user=None):
        if id_user is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(Notify)
            query = query.filter_by(IdUser=id_user)
            resultList = query.all() 
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "Id": result.Id,
                    "IdUser": result.IdUser,
                    "IdSchedule": result.IdSchedule,
                    "Message": result.Message,
                    "DateTimeCreate": result.DateTimeCreate.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeCreate is not None else None,
                    "IsActive": bool(result.IsActive) if result.IsActive is not None else None,
                    "ToWork": bool(result.ToWork) if result.ToWork is not None else None,
                }
                result_dicts.append(result_dict)
            return result_dicts
        
