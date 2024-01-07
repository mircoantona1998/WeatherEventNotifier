from datetime import datetime
from DB.Session import Session
from DB.Model import Key
from Utils.Logger import Logger
import inspect
from datetime import datetime
class ApiKeyRepo:
        
    def get_key():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_key - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            query = session.query(Key)
            return query.first() 
        
   
                      
   