from DB.Session import Session
from DB.Model import TelegramUsers

class TelegramUsersRepo:
    
    def get_all():
        with Session.get_database_session() as session:
            result_list =session.query(TelegramUsers).all()
            result_dicts = []
            for result in result_list:
                result_dict = {
                    "id": result.id,
                    "ChatId": result.chat_id if result.chat_id is not None else None,
                    "idUser": result.idUser,
                    "isActive": bool(result.isActive) if result.isActive is not None else None,
                }
                result_dicts.append(result_dict)
            return result_dicts
        
    def get_user_telegram(idUser):
        if idUser is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(TelegramUsers)
            query = query.filter_by(idUser=idUser)
            result_list = query.all()  
            result_dicts = []
            for result in result_list:
                result_dict = {
                    "id": result.id,
                    "ChatId": result.chat_id if result.chat_id is not None else None,
                    "idUser": result.idUser,
                    "isActive": bool(result.isActive) if result.isActive is not None else None,
                }
                result_dicts.append(result_dict)
            return result_dicts
        
    def add_user_telegram(new_element_data):
        TelegramUsersRepo.delete_element(new_element_data["idUser"])
        with Session.get_database_session() as session:
            new_element = TelegramUsers(**new_element_data)
            session.add(new_element)
            session.commit()
                
    def patch_user_telegram(element_id, patch_data):
        TelegramUsersRepo.delete_element(element_id)
        with Session.get_database_session() as session:
            element_to_patch = session.merge(TelegramUsers(idUser=element_id))
            if element_to_patch:
                if 'chat_id' in patch_data and patch_data['chat_id'] is not None:
                    element_to_patch.chat_id = patch_data['chat_id']
                if 'isActive' in patch_data and patch_data['isActive'] is not None:
                    element_to_patch.isActive = patch_data['isActive']
                session.commit()
 
    def delete_element( id_user=None):
        if id_user is None:
            return
        with Session.get_database_session() as session:
            query = session.query(TelegramUsers)
            if id_user is not None:
                query = query.filter_by(idUser=id_user)
            elements_to_delete = query.all()
            for element_to_delete in elements_to_delete:
                session.delete(element_to_delete)
            session.commit()
