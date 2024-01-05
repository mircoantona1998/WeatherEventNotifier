from DB.Session import Session
from DB.Model import MailConfiguration

class MailConfigurationRepo:
    
    def get():
        with Session.get_database_session() as session:
            query = session.query(MailConfiguration)
            result_list = query.all() 
            for result in result_list:
                result_dict = {
                    "id": result.Id,
                    "mail": result.mail if result.mail is not None else None,
                    "name": result.name if result.name is not None else None,
                    "password": result.password if result.password is not None else None,
                }
                return result_dict
   
    def add_message(data):
        with Session.get_database_session() as session:
            new_message =data
            session.add(new_message)
            session.commit()
  
    def get_latest_message():
        with Session.get_database_session() as session:
            latest_message = session.query(MailConfiguration).order_by(MailConfiguration.id.desc()).first()
            return latest_message

