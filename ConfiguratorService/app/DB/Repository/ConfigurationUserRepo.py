from datetime import datetime, timedelta
from DB.Repository.FrequencyRepo import FrequencyRepo
from DB.Session import Session
from DB.Model import ConfigurationUser
from DB.Model import t_View_ConfigurationUser
from sqlalchemy import text
from Utils.Logger import Logger
import inspect
from datetime import datetime 
class ConfigurationUserRepo:
        
    def get_all_by_user(user_id):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all_by_user  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            resultList = session.query(t_View_ConfigurationUser).filter_by(IdUser=user_id).all()
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "Id": result.Id,
                    "NameConfiguration": result.NameConfiguration,
                    "IdUser": result.IdUser,
                    "IdFrequency": result.IdFrequency,
                    "FrequencyName": result.FrequencyName,
                    "Longitude": float(result.Longitude) if result.Longitude is not None else None,
                    "Latitude": float(result.Latitude) if result.Latitude is not None else None,
                    "DateTimeCreate": result.DateTimeCreate.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeCreate is not None else None,
                    "DateTimeUpdate": result.DateTimeUpdate.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeUpdate is not None else None,
                    "DateTimeActivation": result.DateTimeActivation.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeActivation is not None else None,
                    "IsActive": bool(result.IsActive) if result.IsActive is not None else None,
                    "Field": result.Field,
                    "Description": result.Description,
                    "Parent": result.Parent,
                    "IdMetric": result.IdMetric,
                    "Symbol": result.Symbol,
                    "Value": float(result.Value) if result.Value is not None else None,
                    "ValueUnit": result.ValueUnit,
                }
                result_dicts.append(result_dict)
            return result_dicts
    
    def get_element_by_NameConfiguration(NameConfiguration,idUser):   
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_element_by_NameConfiguration  - {inspect.currentframe().f_globals['__file__']}")
        if NameConfiguration is None:
            return None  
        with Session.get_database_session() as session:
            query = session.query(ConfigurationUser)
            query = query.filter_by(NameConfiguration=NameConfiguration,IdUser=idUser)
            return query.first()  
     
    def get_new_configuration_for_today(idConfiguration):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_new_configuration_for_today  - {inspect.currentframe().f_globals['__file__']}")
        today = datetime.utcnow().replace(hour=0, minute=0, second=0, microsecond=0)
        midnight = today + timedelta(days=1)
        with Session.get_database_session() as session:
            resultList = session.query(t_View_ConfigurationUser).filter_by(Id=idConfiguration).all()
            for result in resultList:
                forNotifier=FrequencyRepo.get_element(result.IdFrequency)
                result_dict = {
                    "Id": result.Id,
                    "NameConfiguration": result.NameConfiguration,                  
                    "IdUser": result.IdUser,
                    "IdFrequency": result.IdFrequency,
                    "FrequencyName": result.FrequencyName,
                    "Longitude": float(result.Longitude) if result.Longitude is not None else None,
                    "Latitude": float(result.Latitude) if result.Latitude is not None else None,
                    "DateTimeCreate": result.DateTimeCreate.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeCreate is not None else None,
                    "DateTimeUpdate": result.DateTimeUpdate.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeUpdate is not None else None,
                    "DateTimeActivation": result.DateTimeActivation.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeActivation is not None else None,
                    "IsActive": bool(result.IsActive) if result.IsActive is not None else None,
                    "Field": result.Field,
                    "IdMetric": result.IdMetric,
                    "Symbol": result.Symbol,
                    "Value": float(result.Value) if result.Value is not None else None,
                    "ValueUnit": result.ValueUnit,
                    "Minutes":forNotifier.Minutes,
                    "Parent":result.Parent,
                    "Description":result.Description,
                }
                return result_dict
        
    def get_all_for_today():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all_for_today  - {inspect.currentframe().f_globals['__file__']}")
        today = datetime.utcnow().replace(hour=0, minute=0, second=0, microsecond=0)
        midnight = today + timedelta(days=1)
        with Session.get_database_session() as session:
            resultList = (
                session.query(t_View_ConfigurationUser)
                .filter(
                    text("DateTimeActivation < :midnight").params(midnight=midnight),
                    t_View_ConfigurationUser.c.IsActive == True 
                )
                .all()
            )
            result_dicts = []
            for result in resultList:
                forNotifier=FrequencyRepo.get_element(result.IdFrequency)
                result_dict = {
                    "Id": result.Id,
                    "NameConfiguration": result.NameConfiguration,  
                    "IdUser": result.IdUser,
                    "IdFrequency": result.IdFrequency,
                    "FrequencyName": result.FrequencyName,
                    "Longitude": float(result.Longitude) if result.Longitude is not None else None,
                    "Latitude": float(result.Latitude) if result.Latitude is not None else None,
                    "DateTimeCreate": result.DateTimeCreate.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeCreate is not None else None,
                    "DateTimeUpdate": result.DateTimeUpdate.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeUpdate is not None else None,
                    "DateTimeActivation": result.DateTimeActivation.strftime('%Y-%m-%d %H:%M:%S') if result.DateTimeActivation is not None else None,
                    "IsActive": bool(result.IsActive) if result.IsActive is not None else None,
                    "Field": result.Field,
                    "IdMetric": result.IdMetric,
                    "Symbol": result.Symbol,
                    "Value": float(result.Value) if result.Value is not None else None,
                    "ValueUnit": result.ValueUnit,
                    "Minutes":forNotifier.Minutes,
                    "Parent":result.Parent,
                    "Description":result.Description,
                }
                result_dicts.append(result_dict)
            return result_dicts
        
    def add_element(new_element_data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - add_element  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            new_element = ConfigurationUser(**new_element_data)
            session.add(new_element)
            session.commit()
            result_dict = {
                    "Id": new_element.Id,
                    "NameConfiguration": new_element.NameConfiguration,  
                    "IdUser": new_element.IdUser,
                    "IdFrequency": new_element.IdFrequency,
                    "Longitude": float(new_element.Longitude) if new_element.Longitude is not None else None,
                    "Latitude": float(new_element.Latitude) if new_element.Latitude is not None else None,
                    "DateTimeCreate": new_element.DateTimeCreate.strftime('%Y-%m-%d %H:%M:%S') if new_element.DateTimeCreate is not None else None,
                    "DateTimeUpdate": new_element.DateTimeUpdate.strftime('%Y-%m-%d %H:%M:%S') if new_element.DateTimeUpdate is not None else None,
                    "DateTimeActivation": new_element.DateTimeActivation.strftime('%Y-%m-%d %H:%M:%S') if new_element.DateTimeActivation is not None else None,
                    "IsActive": bool(new_element.IsActive) if new_element.IsActive is not None else None,
                    "IdMetric": new_element.IdMetric,
                    "Symbol": new_element.Symbol,
                    "Value": float(new_element.Value) if new_element.Value is not None else None,
                }
            return result_dict
                
    def patch_element(element_id, patch_data):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - patch_element  - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            element_to_patch = session.merge(ConfigurationUser(Id=element_id))
            if element_to_patch:
                if 'IdUser' in patch_data and patch_data['IdUser'] is not None:
                    element_to_patch.IdUser = patch_data['IdUser']
                if 'Longitude' in patch_data and patch_data['Longitude'] is not None:
                    element_to_patch.Longitude = patch_data['Longitude']
                if 'Latitude' in patch_data and patch_data['Latitude'] is not None:
                    element_to_patch.Latitude = patch_data['Latitude']
                if 'IdMetric' in patch_data and patch_data['IdMetric'] is not None:
                    element_to_patch.IdMetric = patch_data['IdMetric']
                if 'IdFrequency' in patch_data and patch_data['IdFrequency'] is not None:
                    element_to_patch.IdFrequency = patch_data['IdFrequency']
                if 'DateTimeUpdate' in patch_data and patch_data['DateTimeUpdate'] is not None:
                    element_to_patch.DateTimeUpdate = patch_data['DateTimeUpdate']
                if 'DateTimeActivation' in patch_data and patch_data['DateTimeActivation'] is not None:
                    element_to_patch.DateTimeActivation = patch_data['DateTimeActivation']
                if 'Symbol' in patch_data and patch_data['Symbol'] is not None:
                    element_to_patch.Symbol = patch_data['Symbol']
                if 'Value' in patch_data and patch_data['Value'] is not None:
                    element_to_patch.Value = patch_data['Value']
                if 'IsActive' in patch_data and patch_data['IsActive'] is not None:
                    element_to_patch.IsActive = patch_data['IsActive']
                if 'NameConfiguration' in patch_data and patch_data['NameConfiguration'] is not None:
                    element_to_patch.NameConfiguration = patch_data['NameConfiguration']
                session.commit()
                result_dict = {
                    "Id": element_to_patch.Id,
                    "NameConfiguration": element_to_patch.NameConfiguration,
                    "IdUser": element_to_patch.IdUser,
                    "IdFrequency": element_to_patch.IdFrequency,
                    "Longitude": float(element_to_patch.Longitude) if element_to_patch.Longitude is not None else None,
                    "Latitude": float(element_to_patch.Latitude) if element_to_patch.Latitude is not None else None,
                    "DateTimeCreate": element_to_patch.DateTimeCreate.strftime('%Y-%m-%d %H:%M:%S') if element_to_patch.DateTimeCreate is not None else None,
                    "DateTimeUpdate": element_to_patch.DateTimeUpdate.strftime('%Y-%m-%d %H:%M:%S') if element_to_patch.DateTimeUpdate is not None else None,
                    "DateTimeActivation": element_to_patch.DateTimeActivation.strftime('%Y-%m-%d %H:%M:%S') if element_to_patch.DateTimeActivation is not None else None,
                    "IsActive": bool(element_to_patch.IsActive) if element_to_patch.IsActive is not None else None,
                    "IdMetric": element_to_patch.IdMetric,
                    "Symbol": element_to_patch.Symbol,
                    "Value": float(element_to_patch.Value) if element_to_patch.Value is not None else None,
                }
                return result_dict
            
    def delete_element(id_user=None, id_configuration=None):
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - delete_element  - {inspect.currentframe().f_globals['__file__']}")
        if id_user is None and id_configuration is None:
            return False
        else:
            with Session.get_database_session() as session:
                query = session.query(ConfigurationUser)
                if id_user is not None:
                    query = query.filter_by(IdUser=id_user)
                if id_configuration is not None:
                    query = query.filter_by(Id=id_configuration)
                elements_to_delete = query.all()
                for element_to_delete in elements_to_delete:
                    session.delete(element_to_delete)
                session.commit()
                return True