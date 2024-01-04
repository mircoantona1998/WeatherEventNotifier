from DB.Session import Session
from DB.Model import Mail

class MailRepo:
    
    def get_all_by_user(user_id):
        with Session.get_database_session() as session:
            resultList = session.query(Mail).filter_by(IdUser=user_id).all()
            result_dicts = []
            for result in resultList:
                result_dict = {
                    "Id": result.Id,
                    "IdUser": result.IdUser,
                    "Mittente": result.Mittente,
                    "Destinatario": result.Destinatario,
                    "Oggetto": result.Oggetto,
                    "Testo": result.Testo,
                    "Allegati": bool(result.Allegati) if result.Allegati is not None else None,
                    "DateCreate": result.DateCreate.strftime('%Y-%m-%d %H:%M:%S') if result.DateCreate is not None else None,
                    "WasSent": bool(result.WasSent) if result.WasSent is not None else None,
                    "Result": result.Result,
                }
                result_dicts.append(result_dict)
            return result_dicts

        
    def add_message(data):
        with Session.get_database_session() as session:
            new_message =data
            session.add(new_message)
            session.commit()
  
    def get_latest_message():
        with Session.get_database_session() as session:
            latest_message = session.query(Mail).order_by(Mail.id.desc()).first()
            return latest_message

