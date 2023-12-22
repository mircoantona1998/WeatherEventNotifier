from DB.Session import Session
from DB.Model import ConfigurationUser
from DB.Model import t_View_ConfigurationUser

class ConfigurationUserRepo:
        
    def get_all_by_user(user_id):
        with Session.get_database_session() as session:
            resultList = session.query(t_View_ConfigurationUser).filter_by(IdUser=user_id).all()
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "Id": result.Id,
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
                }
                result_dicts.append(result_dict)
            return result_dicts
    def add_element(new_element_data):
        with Session.get_database_session() as session:
            new_element = ConfigurationUser(**new_element_data)
            session.add(new_element)
            session.commit()
            return new_element.as_dict()
                
    def patch_element(element_id, patch_data):
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
                session.commit()
                return element_to_patch.as_dict()
            
    def delete_element(id_user=None, id_configuration=None):
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