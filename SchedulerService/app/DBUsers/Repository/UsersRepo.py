from DBUsers.Session import Session
from DBUsers.Model import Users
from Utils.Logger import Logger
import inspect
from Configurations.Configurations import Configurations
from datetime import datetime
class UsersRepo:
    
    def get():
        Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - get_all - {inspect.currentframe().f_globals['__file__']}")
        with Session.get_database_session() as session:
            return session.query(Users).filter_by(Partition=int(Configurations().partition), Cluster=int(Configurations().cluster)).all()
   
  

