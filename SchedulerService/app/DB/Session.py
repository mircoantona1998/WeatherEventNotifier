from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker
from Configurations.Configurations import Configurations
import time
from datetime import datetime 
from sqlalchemy.exc import OperationalError
from Utils.Logger import Logger
import inspect
class Session:
    def get_database_session():
        connection_string = f'mysql://{Configurations().database_username}:{Configurations().database_password}@{Configurations().database_ip}:{Configurations().database_porta}/{Configurations().database_name}'
        engine = create_engine(connection_string, echo=False)
        Session = sessionmaker(bind=engine)
        return Session()
    
    def wait_for_mysql(timeout=60):
        start_time = time.time()
        while True:
            try:
                engine = create_engine(f'mysql://{Configurations().database_username}:{Configurations().database_password}@{Configurations().database_ip}:{Configurations().database_porta}/{Configurations().database_name}')
                connection = engine.connect()
                connection.close()
                print("Database is available!")
                Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - Database is available! - {inspect.currentframe().f_globals['__file__']}")
                break
            except OperationalError as e:
                print(f"Database not available yet. Retrying in 5 seconds... Error: {str(e)}")
                Logger().log_action(f"{str(datetime.utcnow().strftime('%d-%m-%Y %H:%M:%S'))} - Database not available yet. Retrying in 5 seconds... Error: {str(e)} - {inspect.currentframe().f_globals['__file__']}")
                time.sleep(5)