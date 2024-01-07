from DB.Session import Session
from DB.Model import MailUsers
from Utils.Logger import Logger
import inspect
from datetime import datetime
class MailUsersRepo:
    
    def get_all():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            result_list =session.query(MailUsers).all()
            result_dicts = []
            for result in result_list:
                result_dict = {
                    "id": result.id,
                    "mail": result.mail if result.mail is not None else None,
                    "idUser": result.idUser,
                    "isActive": bool(result.isActive) if result.isActive is not None else None,
                }
                result_dicts.append(result_dict)
            return result_dicts
        
    def get_user(idUser):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_user - {inspect.currentframe().f_globals['__file__']}")
        if idUser is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(MailUsers)
            query = query.filter_by(idUser=idUser, isActive=True)
            result_list = query.all() 
            if len(result_list)==0:
                return result_list 
            result_dicts = []
            for result in result_list:
                result_dict = {
                    "id": result.id,
                    "mail": result.mail if result.mail is not None else None,
                    "idUser": result.idUser,
                    "isActive": bool(result.isActive) if result.isActive is not None else None,
                }
                result_dicts.append(result_dict)
                return result_dicts
            
    def get_user_mail(idUser):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_user_mail - {inspect.currentframe().f_globals['__file__']}")
        if idUser is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(MailUsers)
            query = query.filter_by(idUser=idUser, isActive=True)
            result_list = query.all() 
            for result in result_list:
                result_dict = {
                    "id": result.id,
                    "mail": result.mail if result.mail is not None else None,
                    "idUser": result.idUser,
                    "isActive": bool(result.isActive) if result.isActive is not None else None,
                }
                return result_dict
            
    def add_user_mail(new_element_data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_user_mail - {inspect.currentframe().f_globals['__file__']}")
        MailUsersRepo.delete_element(new_element_data["idUser"])
        with Session.get_database_session() as session:
            new_element = MailUsers(**new_element_data)
            session.add(new_element)
            session.commit()
                
    def patch_user_mail(element_id, patch_data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - patch_user_mail - {inspect.currentframe().f_globals['__file__']}")
        MailUsersRepo.delete_element(element_id)
        with Session.get_database_session() as session:
            element_to_patch = session.merge(MailUsers(idUser=element_id))
            if element_to_patch:
                if 'mail' in patch_data and patch_data['mail'] is not None:
                    element_to_patch.mail = patch_data['mail']
                if 'isActive' in patch_data and patch_data['isActive'] is not None:
                    element_to_patch.isActive = patch_data['isActive']
                session.commit()
 

    def delete_element( id_user=None):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - delete_element - {inspect.currentframe().f_globals['__file__']}")
        if id_user is None:
            return
        with Session.get_database_session() as session:
            query = session.query(MailUsers)
            if id_user is not None:
                query = query.filter_by(idUser=id_user)
            elements_to_delete = query.all()
            for element_to_delete in elements_to_delete:
                session.delete(element_to_delete)
            session.commit()