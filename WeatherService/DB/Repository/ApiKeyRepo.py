from datetime import datetime
from DB.Session import Session
from DB.Model import Key

class ApiKeyRepo:
        
    def get_key():
        with Session.get_database_session() as session:
            query = session.query(Key)
            return query.first() 
        
   
                      
   