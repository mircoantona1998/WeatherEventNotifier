from DB.Session import Session
from DB.Model import TelegramConfiguration

class TelegramConfigurationRepo:
    
    def get():
        with Session.get_database_session() as session:
            query= session.query(TelegramConfiguration)
            result_list = query.all()  
            for result in result_list:
                result_dict = {
                    "id": result.Id,
                    "bot": result.bot if result.bot is not None else None,
                    "token":result.token if result.token is not None else None,
                }
                return result_dict
   
    def add_message(data):
        with Session.get_database_session() as session:
            new_message =data
            session.add(new_message)
            session.commit()
  
    def get_latest_message():
        with Session.get_database_session() as session:
            latest_message = session.query(TelegramConfiguration).order_by(TelegramConfiguration.id.desc()).first()
            return latest_message

